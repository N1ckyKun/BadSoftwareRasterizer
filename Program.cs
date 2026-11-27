using System.Runtime.CompilerServices;
using Raylib_cs;
using static System.Math;
using Rasterizer.Types;

namespace Rasterizer;

internal static class Program
{
    public static string GetSourceDirectory([CallerFilePath] string filePath = "")
        => Path.GetDirectoryName(filePath)!;

    [System.STAThread]
    public static void Main()
    {
        int width = 1280;
        int height = 720;
        Raylib.SetTargetFPS(60);
        Raylib.InitWindow(width, height, "Rasterizer");
        Texture2D texture = Raylib.LoadTextureFromImage(
            Raylib.GenImageColor(width, height, Color.Black)
        );

        string sourceDir = GetSourceDirectory();
        string objPath = Path.Combine(sourceDir, "Rasterizer","Objects", "monkey.obj");
        string objString = File.ReadAllText(objPath);

        float3[] objPoints = ObjHandler.LoadObjFile(objString);
        Obj Model = ObjHandler.CreateObj(objPoints);

        RenderTarget renderTarget = new(width, height);
        Transform transform = new();
        transform.Position = new float3(0,0,3);
        
        while (!Raylib.WindowShouldClose())
        {
            transform.rotate(5);
            Raylib.UpdateTexture(texture, Renderer.Render(Model,renderTarget,transform));
            
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.DrawTexture(texture, 0, 0, Color.White);
            Raylib.DrawFPS(10, 10);
            Raylib.EndDrawing();
        }
        Raylib.UnloadTexture(texture);
        Raylib.CloseWindow();
    }
}