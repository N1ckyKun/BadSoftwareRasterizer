using Rasterizer.Types;
using static System.Math;

namespace Rasterizer;

class ScreenMath
{
    public static float3 VertexToScreen(float3 vertex, float2 numPixels, Transform transform)
    {
        float3 vertexWorld = transform.ToWorldPoint(vertex);
        float worldHeight = (float)Tan(30/2) * 2;
        float pixelsPerUnit = (numPixels.y / worldHeight) / vertexWorld.z;
        
        float2 pixelOffset = new float2(vertexWorld.x,vertexWorld.y) * pixelsPerUnit;
        float2 vertexScreen = numPixels/2f + pixelOffset; 
        return new float3(vertexScreen.x,vertexScreen.y, vertexWorld.z);
    }    
}

public class Transform
{
    
    public float yaw = (float)PI;
    public float pitch;
    public float3 Position;
    public void rotate(float yawdeg = 0, float pitchdeg = 0)
    {
        yaw += yawdeg*(float)PI/180;
        pitch += pitchdeg*(float)PI/180;
    }
    public float3 ToWorldPoint(float3 p)
    {
        (float3 ihat, float3 jhat, float3 khat) = GetBasisVectors();
        return TransformVector(ihat, jhat, khat, p) + Position;
    }

    (float3 ihat, float3 jhat, float3 khat) GetBasisVectors()
    {
        float3 iyaw = new((float)Cos(yaw),0,(float)Sin(yaw));
        float3 jyaw = new(0,1,0);
        float3 kyaw = new(-(float)Sin(yaw),0,(float)Cos(yaw));
        float3 ipit = new(1,0,0);
        float3 jpit = new(0, (float)Cos(pitch), -(float)Sin(pitch));
        float3 kpit = new(0, (float)Sin(pitch), (float)Cos(pitch));
        float3 ihat = TransformVector(iyaw,jyaw,kyaw,ipit);
        float3 jhat = TransformVector(iyaw,jyaw,kyaw,jpit);
        float3 khat = TransformVector(iyaw,jyaw,kyaw,kpit);
        return (ihat,jhat,khat);
    }

    float3 TransformVector(float3 ihat, float3 jhat, float3 khat, float3 p)
    {
        return new float3(p.x)*ihat + new float3(p.y)*jhat + new float3(p.z)*khat;
    }
}