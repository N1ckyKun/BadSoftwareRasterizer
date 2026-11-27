namespace Rasterizer.Types;

public struct float3 {
    public float x;
    public float y;
    public float z;

    public float3(float x, float y, float z) {
        this.x = x;
        this.y = y;
        this.z = z;
    }
    public float3(float a) {
        this.x = a;
        this.y = a;
        this.z = a;
    }

    public static float3 operator +(float3 a, float3 b)
        => new float3(a.x + b.x, a.y + b.y, a.z + b.z);
    
    public static float3 operator -(float3 a, float3 b)
        => new float3(a.x + b.x, a.y + b.y, a.z + b.z);

    public static float3 operator *(float3 a, float3 b)
        => new float3(a.x * b.x, a.y * b.y, a.z * b.z);

    public static float3 operator /(float3 a, float3 b)
        => new float3(a.x / b.x, a.y / b.y, a.z / b.z);

    public static float Dot(float3 a, float3 b)
        => a.x*b.x + a.y*b.y + a.z*b.z;

    public static explicit operator float2(float3 p)
        => new(p.x, p.y);
}