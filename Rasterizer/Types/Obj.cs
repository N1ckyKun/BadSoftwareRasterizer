namespace Rasterizer.Types;

public class Obj(float3[] points, float3[] cols)
{
    public readonly float3[] Points = points;
    public readonly float3[] TriangleCols = cols;
}