using System;
using System.Numerics;
using Raylib_cs;

namespace bouncing_balls;

public class Ball
{
    public Vector2 Position;
    public Vector2 Velocity;
    public Color Color;
    public int Radius;

    public Ball(Vector2 position, Vector2 velocity, Color color, int radius)
    {
        Position = position;
        Velocity = velocity;
        Color = color;
        Radius = radius;
    }

    public void Update()
    {
        Position += Velocity;
    }

    public void Draw()
    {
        Raylib.DrawCircleV(Position, Radius, Color);
    }
}