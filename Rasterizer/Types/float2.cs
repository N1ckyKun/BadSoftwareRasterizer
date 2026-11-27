namespace Rasterizer.Types;

public struct float2 {
    public float x;
    public float y;

    public float2(float x, float y){
        this.x = x;
        this.y = y;
    }
    public float2(float a){
        this.x = a;
        this.y = a;
    }

    public static float2 operator +(float2 a, float2 b)
        => new(a.x + b.x, a.y + b.y);

    public static float2 operator -(float2 a, float2 b)
        => new(a.x - b.x, a.y - b.y);

    public static float2 operator *(float2 p, float mult)
        => new(p.x * mult, p.y * mult);

    public static float2 operator /(float2 p, float mult)
        => new(p.x / mult, p.y / mult);

    public static float Dot(float2 a, float2 b)
        => a.x*b.x + a.y*b.y;
}