using UnityEngine;
using UnityEngine.UIElements;

namespace CuteNewtTest.MapGeneration.Strategies
{
    public class RandomPointStrategy : IMapGenerationStrategy
    {
        [SerializeField] float _spawnChance;

        public void GenerateMap(AMapLayer map)
        {
            map.ForEachPosition(position =>
            {
                if (CheckForSpawnChance() && CheckMinimunOffset(position, map))
                {
                    map.CreateMainTile(position);
                }
            });
        }

        bool CheckMinimunOffset(Vector3Int position, AMapLayer map)
        {
            return map.GetTileCountInNeighbours(position, new Vector2Int(2,2)) == 0;
        }

        public bool CheckForSpawnChance()
        {
            return Random.value < _spawnChance / 100f;
        }
    }
}