using UnityEngine.Tilemaps;

namespace CuteNewtTest.MapGeneration.Strategies
{
    public interface IMapGenerationStrategy
    {
        public void GenerateMap(AMapLayer map);
    }
}