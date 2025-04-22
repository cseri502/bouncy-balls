using System;

namespace bouncing_balls;

public static class Config
{
    public const int ScreenWidth = 1280;
    public const int ScreenHeight = 720;
    public const string Title = "Ball Simulation";

    public const int CircleRadius = 200;
    public const int CircleThickness = 3;
    public const float GapStartAngle = 0.2f;
    public const float GapEndAngle = 1.0f;

    public const int SmallCircleRadius = 15;
    public const float MinSpeed = 2.0f;
    public const float MaxSpeed = 5.0f;
}
