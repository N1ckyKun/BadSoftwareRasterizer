using Rasterizer.Types;
using static System.Math;
using Raylib_cs;

namespace Rasterizer;

class Renderer
{
    public static Color[] ToColor(float3[] px)
    {
        Color[] ColorList = new Color[px.Length];
        for(int i = 0; i < px.Length; i++)
        {
            float3 curpx = px[i];
            Color c = new(
                (byte)(curpx.x * 255),
                (byte)(curpx.y * 255),
                (byte)(curpx.z * 255),
                (byte)255
            );
            ColorList[i] = c;
        }
        return ColorList;
    }

    public static Color[] Render(Obj model, RenderTarget target, Transform transform)
    {
        int height = target.Height;
        int width = target.Width;
        for(int i = 0; i<height*width; i++) target.ColorBuffer[i] = new(0,0,0);
        for(int i = 0; i<height*width; i++) target.DepthBuffer[i] = 9999;
        for(int i = 0; i<model.Points.Length; i+=3)
        {
            float3 a = ScreenMath.VertexToScreen(model.Points[i], target.Size, transform);
            float3 b = ScreenMath.VertexToScreen(model.Points[i+1], target.Size, transform);
            float3 c = ScreenMath.VertexToScreen(model.Points[i+2], target.Size, transform);

            float minX = Min(Min(a.x,b.x),c.x);
            float maxX = Max(Max(a.x,b.x),c.x);
            float minY = Min(Min(a.y,b.y),c.y);
            float maxY = Max(Max(a.y,b.y),c.y);

            int BlockStartX = Clamp((int)minX, 0, width-1);
            int BlockEndX = Clamp((int)Ceiling(maxX), 0, width-1);
            int BlockStartY = Clamp((int)minY, 0, height-1);
            int BlockEndY = Clamp((int)Ceiling(maxY), 0, height-1);

            for(int y = BlockStartY; y<=BlockEndY; y++) {
                for(int x = BlockStartX; x<BlockEndX; x++) {
                    float3 weights;
                    if (Math.PointInTriangle((float2)a,(float2)b,(float2)c,new float2(x,y),out weights))
                    {
                        float3 depths = new(a.z, b.z, c.z);
                        float depth = float3.Dot(depths, weights);
                        if(depth > target.DepthBuffer[y * width + x]) continue;
                        target.DepthBuffer[y * width + x] = depth;
                        target.ColorBuffer[y * width + x] = model.TriangleCols[i/3];
                    }
                }
            }
        }
        return ToColor(target.ColorBuffer);
    }
}