
using System.Numerics;

namespace Frank.Games.SpaceLaneTycoon;

public class Camera
{
    public Vector3 Position { get; set; }
    public Vector3 Forward { get; private set; }
    public Vector3 Up { get; private set; }
    public float ZoomLevel { get; set; } = 1.0f;

    public Camera(Vector3 position)
    {
        Position = position;
        Forward = Vector3.UnitZ; // Assuming +Z is forward in this example
        Up = Vector3.UnitY; // Assuming +Y is up
    }

    public Matrix4x4 GetViewMatrix()
    {
        return Matrix4x4.CreateLookAt(Position, Position + Forward, Up);
    }

    public Matrix4x4 GetProjectionMatrix(float width, float height)
    {
        float aspectRatio = width / height;
        // Adjust FOV and near/far planes as needed
        return Matrix4x4.CreatePerspectiveFieldOfView(MathF.PI / 4, aspectRatio, 0.1f, 100.0f) * Matrix4x4.CreateScale(ZoomLevel);
    }

    public void Zoom(float amount)
    {
        ZoomLevel *= amount;
    }

    public void Rotate(float pitch, float yaw)
    {
        // Simple rotation around the Y axis for yaw and X axis for pitch
        Matrix4x4 rotation = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, yaw) * Matrix4x4.CreateFromAxisAngle(Vector3.UnitX, pitch);
        Forward = Vector3.TransformNormal(Forward, rotation);
        Up = Vector3.TransformNormal(Up, rotation);
    }
}
