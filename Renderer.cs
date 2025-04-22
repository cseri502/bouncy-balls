using System;
using Raylib_cs;
using System.Numerics;

namespace bouncing_balls;

public static class Renderer
{
    public static void DrawCircleWithGap(Vector2 center)
    {
        for (float angle = 0; angle < MathF.PI * 2; angle += 0.01f)
        {
            if (angle >= Config.GapStartAngle && angle <= Config.GapEndAngle) continue;

            Vector2 p1 = center + new Vector2(MathF.Cos(angle), MathF.Sin(angle)) * Config.CircleRadius;
            Vector2 p2 = center + new Vector2(MathF.Cos(angle + 0.01f), MathF.Sin(angle + 0.01f)) * Config.CircleRadius;

            Raylib.DrawLineEx(p1, p2, Config.CircleThickness, Color.White);
        }
    }
}
