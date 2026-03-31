using System.Numerics;

using Frank.Games.SpaceLaneTycoon;
using Frank.Games.SpaceLaneTycoon.Mapping;

using Microsoft.Extensions.Hosting;

using Raylib_cs;

public class WindowHost : BackgroundService
{
    private readonly StarCache _starCache;
    
    
    public unsafe WindowHost(StarCache starCache)
    {
        _starCache = starCache;
        
        var title = "Space Lane Tycoon";
        var aspectRatio = 16f / 9f;
        var width = 1280;
        var height = (int) (width / aspectRatio);
        Raylib.InitWindow(width, height, title);
        Raylib.SetTargetFPS(60);
        Raylib.SetExitKey(KeyboardKey.KEY_ESCAPE);
    }
        
    
        
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            Raylib.BeginDrawing();

            Raylib.ClearBackground(Color.BLACK);
            Raylib.DrawText("Space Lane Tycoon", 10, 10, 20, Color.WHITE);

            // Ensure the camera is updated each frame
            SetupCamera();

            foreach (var star in _starCache.Stars.OrderBy(x => x.Id).Take(2500))
            {
                // Make sure the star's position is translated correctly for the camera view
                Vector3 screenPosition = TranslateStarPosition(star.Position);

                // Scale the radius so it's visually meaningful
                float visualRadius = ScaleRadiusForVisualization(star.Radius);

                Raylib.DrawSphere(screenPosition, visualRadius, star.Color.ToRaylibColor());
            }

            Raylib.EndDrawing();
        }
    }

    private Vector3 TranslateStarPosition(Vector3 starPosition)
    {
        // Here you'd translate the star position from its space coordinates to screen coordinates
        
        throw new NotImplementedException();
    }

    private float ScaleRadiusForVisualization(float starRadius)
    {
        // Scale the star radius so it's visually meaningful, e.g., between a minimum and maximum size
        
        throw new NotImplementedException();
    }

    protected async Task NotExecuteAsync(CancellationToken stoppingToken)
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Space Lane Tycoon", 10, 10, 20, Color.WHITE);
        SetupCamera();

        // Offset the origin to the center of the screen
        Raylib.DrawGrid(10, 1.0f);
            
            
        foreach (var star in _starCache.Stars.OrderBy(x => x.Id).Take(2500))
        {
            Console.WriteLine($"Drawing star {star.Id} at {star.Position} with radius {star.Radius}");
                
            Raylib.DrawSphere(star.Position, NormalizeValue(star.Radius), star.Color.ToRaylibColor());
        }
            
        Raylib.DrawCircle(0, 0, 15.9f, Color.PURPLE);
            
        Raylib.EndDrawing();
    }
    
    public static float NormalizeValue(float value, float min = -1.0f, float max = 1.0f)
    {
        return 2f * ((value - min) / (max - min)) - 1f;
    }
    
    private static double Calculate3DDistanceFromCenter(Vector3 starPosition)
    {
        return Math.Sqrt(starPosition.X * starPosition.X + starPosition.Y * starPosition.Y + starPosition.Z * starPosition.Z);
    }

    private unsafe void SetupCamera()
    {
        
        Camera3D* camera = stackalloc Camera3D[1];
        
        camera->Position = new Vector3(640.0f, 10.0f, 10.0f); // Camera position
        camera->Target = new Vector3(0.0f, 0.0f, 0.0f);      // Camera looking at point
        camera->Up = new Vector3(0.0f, 1.0f, 0.0f);           // Camera up vector (rotation towards target)
        camera->FovY = 45.0f;                                       // Camera field-of-view Y
        
        Raylib.UpdateCamera(camera, CameraMode.CAMERA_ORBITAL);
    }
}