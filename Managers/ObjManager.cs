using Rasterizer.Types;

namespace Rasterizer;

class ObjHandler()
{
    static string[] SplitByLine(String objString)
    {
        String[] stringList = objString.Split("\n");
        return stringList;
    }

    public static float3[] LoadObjFile(string objString)
    {
        List<float3> allPoints = new();
        List<float3> trianglePoints = new();

        foreach (string line in SplitByLine(objString))
        {
            if(line.StartsWith("v "))
            {
                float[] axes = line[2..].Split(" ").Select(float.Parse).ToArray();
                allPoints.Add(new float3(axes[0],axes[1],axes[2]));
            }
            else if(line.StartsWith("f "))
            {
                String[] faceSegment = line[2..].Split(' ');
                for (int i = 0; i<faceSegment.Length; i++)
                {
                    String[] indexGroupString = faceSegment[i].Split("/");
                    int vertexIndex = int.Parse(indexGroupString[0]) - 1;

                    if(i >= 3) trianglePoints.Add(trianglePoints[^(3 * i - 6)]);
                    if(i >= 3) trianglePoints.Add(trianglePoints[^2]);
                    trianglePoints.Add(allPoints[vertexIndex]);
                }
            }
        }
        return trianglePoints.ToArray();
    }

    public static Obj CreateObj(float3[] objModelPoints)
    {
        Random rng = new(75);
        float3[] triangleCols = new float3[objModelPoints.Length/3];
        for(int i = 0; i<triangleCols.Length; i++)
            triangleCols[i] = Randomizer.RandomColor(rng);
        Obj objModel = new(objModelPoints, triangleCols);
        return objModel;
    }
}
