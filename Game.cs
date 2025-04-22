using System;
using System.Numerics;
using Raylib_cs;

namespace bouncing_balls;

public class Game
{
    private readonly List<Ball> balls = [];
    private readonly Vector2 circleCenter = new(Config.ScreenWidth / 2, Config.ScreenHeight / 2);
    private readonly Random random = new();

    public Game()
    {
        balls.Add(CreateRandomBall());
    }

    public void Update()
    {
        List<Ball> escaped = [];

        foreach (var ball in balls)
        {
            ball.Update();

            if (IsBallEscapingThroughGap(ball) || IsBallOutsideCircle(ball))
            {
                escaped.Add(ball);
            }
            else
            {
                HandleCollision(ball);
            }
        }

        foreach (var ball in escaped)
        {
            balls.Remove(ball);
            for (int i = 0; i < 3; i++)
            {
                balls.Add(CreateRandomBall());
            }
        }
    }

    public void Draw()
    {
        Renderer.DrawCircleWithGap(circleCenter);
        foreach (var ball in balls) ball.Draw();

        Raylib.DrawText($"Ball count: {balls.Count}", 10, 10, 20, Color.White);
        Raylib.DrawText($"FPS: {Raylib.GetFPS()}", 10, 40, 20, Color.RayWhite);
    }

    private Ball CreateRandomBall()
    {
        return new Ball(
            GetRandomPosition(),
            GetRandomVelocity(),
            GetRandomColor(),
            Config.SmallCircleRadius
        );
    }

    private Color GetRandomColor()
    {
        return new Color(random.Next(100, 255), random.Next(100, 255), random.Next(100, 255), 255);
    }

    private Vector2 GetRandomPosition()
    {
        float angle = (float)(random.NextDouble() * 2 * Math.PI);
        float distance = (float)(random.NextDouble() * (Config.CircleRadius - Config.SmallCircleRadius - 20));
        
        return circleCenter + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * distance;
    }

    private Vector2 GetRandomVelocity()
    {
        float speed = (float)(random.NextDouble() * (Config.MaxSpeed - Config.MinSpeed) + Config.MinSpeed);
        float angle = (float)(random.NextDouble() * 2 * Math.PI);
        
        return new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * speed;
    }

    private bool IsBallEscapingThroughGap(Ball ball)
    {
        Vector2 dir = ball.Position - circleCenter;
        float distance = dir.Length();

        if (Math.Abs(distance - Config.CircleRadius) <= Config.SmallCircleRadius + Config.CircleThickness)
        {
            float angle = (float)Math.Atan2(dir.Y, dir.X);
            if (angle < 0) angle += 2 * MathF.PI;

            if (angle >= Config.GapStartAngle && angle <= Config.GapEndAngle)
            {
                float dot = Vector2.Dot(Vector2.Normalize(dir), Vector2.Normalize(ball.Velocity));
                return dot > 0.3f && distance >= Config.CircleRadius - Config.CircleThickness;
            }
        }

        return false;
    }

    private bool IsBallOutsideCircle(Ball ball)
    {
        return Vector2.Distance(ball.Position, circleCenter) > Config.CircleRadius + ball.Radius;
    }

    private void HandleCollision(Ball ball)
    {
        Vector2 toBall = ball.Position - circleCenter;
        float distance = toBall.Length();
        float angle = (float)Math.Atan2(toBall.Y, toBall.X);
        if (angle < 0) angle += 2 * MathF.PI;

        if (distance > Config.CircleRadius - ball.Radius - Config.CircleThickness && !(angle >= Config.GapStartAngle && angle <= Config.GapEndAngle))
        {
            Vector2 normal = Vector2.Normalize(toBall);
            float dot = 2 * Vector2.Dot(ball.Velocity, -normal);
            ball.Velocity += dot * normal;

            float correction = Config.CircleRadius - ball.Radius - Config.CircleThickness - distance;
            ball.Position += normal * correction;
        }
    }
}
