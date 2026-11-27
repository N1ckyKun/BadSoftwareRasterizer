using Rasterizer.Types;

namespace Rasterizer;

class Randomizer
{
    public static float3 RandomColor(Random rng)
        => new((float)rng.NextDouble(),(float)rng.NextDouble(),(float)rng.NextDouble());
}