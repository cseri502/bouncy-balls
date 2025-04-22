using Raylib_cs;

namespace bouncing_balls;

class Program
{
    static void Main()
    {
        Raylib.InitWindow(Config.ScreenWidth, Config.ScreenHeight, Config.Title);
        Raylib.SetTargetFPS(60);

        Game game = new();

        while (!Raylib.WindowShouldClose())
        {
            game.Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            game.Draw();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
