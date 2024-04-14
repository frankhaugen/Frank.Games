using Silk.NET.OpenGL;
using Silk.NET.Windowing;

using System.Numerics;

using Silk.NET.Input;

namespace Frank.Games.SpaceLaneTycoon;

public class MainWindow
{
    private readonly StarCache _starCache;
    private GL _gl;
    private readonly IWindow _window;
    private IInputContext _input;
    private IMouse _mouse;
    private float _zoomLevel = 1.0f;
    private const float DefaultZoomLevel = 1.0f;
    private uint _shaderProgram;
    private uint _vertexArrayObject;
    private uint _vertexBufferObject;
    private int _zoomUniformLocation;
    private Camera _camera;


    public MainWindow(StarCache starCache)
    {
        _starCache = starCache;
        var options = WindowOptions.Default;
        options.Size = new Silk.NET.Maths.Vector2D<int>(800, 600);
        options.Title = "Sol Starmap";
        
        _window = Window.Create(options);
        _window.Load += OnLoad;
        _window.Render += OnRender;
    }

    private void OnMouseDown(IMouse arg1, MouseButton arg2)
    {
        if (arg2 == MouseButton.Middle)
        {
            _zoomLevel = DefaultZoomLevel;
        }
    }

    private void OnMouseScroll(IMouse mouse, ScrollWheel scrollWheel)
    {
        _camera.Zoom(1.0f + scrollWheel.Y * 50.0f); // Adjust the 0.05f value based on desired sensitivity
    }

    private void OnLoad()
    {
        _gl = GL.GetApi(_window);
        
        _input = _window.CreateInput();
        
        _mouse = _input.Mice.First();
        _mouse.Scroll += OnMouseScroll;
        _mouse.MouseDown += OnMouseDown;
        
        // Initialize camera at position (0, 0, 5) looking towards the origin
        _camera = new Camera(new Vector3(0, 0, 5));

        _gl.Disable(GLEnum.DepthTest);
        
        _zoomUniformLocation = (int)_gl.GetUniformLocation(_shaderProgram, "zoom");

        _shaderProgram = CreateShaderProgram();
        SetupVertexArrayObject();

        // Assuming your stars have a Position property of type Vector3
        var positions = _starCache.Stars.Select(star => star.Postition).ToArray();
        LoadStarPositionsIntoBuffer(positions);
    }

    private unsafe void SetupVertexArrayObject()
    {
        _vertexArrayObject = _gl.GenVertexArray();
        _gl.BindVertexArray(_vertexArrayObject);

        _vertexBufferObject = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
        _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), null);
        _gl.EnableVertexAttribArray(0);
    }

    private void LoadStarPositionsIntoBuffer(Vector3[] positions)
    {
        var data = positions.SelectMany(position => new[] { position.X, position.Y, position.Z }).ToArray().AsSpan();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vertexBufferObject);
        _gl.BufferData<float>(BufferTargetARB.ArrayBuffer, (uint)(positions.Length * 3 * sizeof(float)), data, BufferUsageARB.StaticDraw);
    }

    private uint CreateShaderProgram()
    {
        var vertexShader = CompileShader(ShaderType.VertexShader, @"
#version 330 core
layout(location = 0) in vec3 aPosition;
uniform float zoom;
uniform mat4 view;
uniform mat4 projection;

void main()
{
    gl_Position = vec4(aPosition * zoom, 100.0);
}");
        var fragmentShader = CompileShader(ShaderType.FragmentShader, @"
#version 330 core
out vec4 FragColor;

void main()
{
    FragColor = vec4(1.0, 1.0, 0.0, 1.0); // Yellow
}");

        var program = _gl.CreateProgram();
        _gl.AttachShader(program, vertexShader);
        _gl.AttachShader(program, fragmentShader);
        _gl.LinkProgram(program);

        // Check for linking errors...

        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);

        return program;
    }

    private uint CompileShader(ShaderType type, string source)
    {
        var shader = _gl.CreateShader(type);
        _gl.ShaderSource(shader, source);
        _gl.CompileShader(shader);

        // Check for compilation errors...

        return shader;
    }

    private unsafe void OnRender(double delta)
    {
        _gl.Clear((uint)ClearBufferMask.ColorBufferBit);
        _gl.ClearColor(0, 0, 0, 1);
        _gl.UseProgram(_shaderProgram);

        // Set camera uniforms
        int viewLocation = _gl.GetUniformLocation(_shaderProgram, "view");
        Matrix4x4 viewMatrix = _camera.GetViewMatrix();
        _gl.UniformMatrix4(viewLocation, 1, false, (float*)&viewMatrix);

        int projectionLocation = _gl.GetUniformLocation(_shaderProgram, "projection");
        Matrix4x4 projectionMatrix = _camera.GetProjectionMatrix(_window.Size.X, _window.Size.Y);
        _gl.UniformMatrix4(projectionLocation, 1, false, (float*)&projectionMatrix);

        _gl.BindVertexArray(_vertexArrayObject);
        _gl.DrawArrays(PrimitiveType.Points, 0, (uint)_starCache.Stars.Count());
        _window.SwapBuffers();
    }


    string GetShaderInfoLog(GL gl, uint shader)
    {
        gl.GetShader(shader, ShaderParameterName.CompileStatus, out var status);
        return status == 1 ? "Compilation success" : gl.GetShaderInfoLog(shader);
    }

    string GetProgramInfoLog(GL gl, uint program)
    {
        gl.GetProgram(program, GLEnum.LinkStatus, out var status);
        return status == 1 ? "Linking success" : gl.GetProgramInfoLog(program);
    }

    public async Task RunAsync(CancellationToken stoppingToken)
    {
        _window.Run();
        await Task.CompletedTask;
    }
}