namespace Rasterizer;

using Rasterizer.Types;

class Math
{
    static float2 Perpendicular(float2 vector)
        => new(vector.y,-vector.x);
    public static Boolean PointInTriangle(float2 a, float2 b, float2 c, float2 p, out float3 weights)
    {
        float ABP = SignedTriangleArea(a,b,p);
        float BCP = SignedTriangleArea(b,c,p);
        float CAP = SignedTriangleArea(c,a,p);
        bool inTri = ABP >= 0 && BCP >= 0 && CAP >= 0;

        float invAreaSum = 1 / (ABP + BCP + CAP);
        float weightA = BCP * invAreaSum;
        float weightB = CAP * invAreaSum;
        float weightC = ABP * invAreaSum;
        weights = new float3(weightA,weightB,weightC);

        return inTri;
    }
    public static float SignedTriangleArea(float2 a, float2 b, float2 c)
    {
        float2 ac = c - a;
        float2 abPerp = Perpendicular(b - a);
        return float2.Dot(ac, abPerp) / 2;
    }
}