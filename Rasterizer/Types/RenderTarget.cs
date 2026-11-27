namespace Rasterizer.Types;

public class RenderTarget(int width,int height)
{
    public float3[] ColorBuffer = new float3[width*height];
    public float[] DepthBuffer = new float[width*height];
    public readonly int Width = width;
    public readonly int Height = height;
    public readonly float2 Size = new(width,height);
}