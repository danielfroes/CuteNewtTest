using CuteNewtTest.Utils;
using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class MapGenerator : MonoBehaviour, IHeightResolver
    {
        [SerializeField] MapData _mapData;
        [SerializeField] List<HeightLevelConfiguration> _heightLevelsData;

        List<HeightLevel> _heightLevels = new();

        [Button()]
        public void GenerateMap()
        {
            ClearPreviousMap();
            GenerateLevels();
        }

        void GenerateLevels()
        {
            HeightLevel currentLevel = CreateHeightLevel(0, null);

            for (int i = 1; i < _heightLevelsData.Count + 1; i++)
            {
                _heightLevels.Add(currentLevel);

                HeightLevel levelAbove = CreateHeightLevel(i, currentLevel);
                currentLevel.CreateProps(levelAbove?.Ground);
                
                currentLevel = levelAbove;
            }
        }

        HeightLevel CreateHeightLevel(int heightIndex, HeightLevel baseHeightLevel)
        {
            if (!_heightLevelsData.TryGetElement(heightIndex, out HeightLevelConfiguration data))
                return null;

            HeightLevel level = new(data, _mapData, heightIndex);
            level.Generate(baseHeightLevel?.Ground);

            return level;
        }

        void ClearPreviousMap()
        {
            _heightLevels.ForEach(layer => layer?.Dispose());
            _heightLevels.Clear();

            for (int i = _mapData.TilemapParent.childCount - 1; i >= 0; i--)
            {
                GameObject child = _mapData.TilemapParent.GetChild(i).gameObject;
                DestroyImmediate(child);
            }
        }

        public int GetHeightIndexInPosition(Vector3 position)
        {
            for (int i = _heightLevels.Count - 1; i >= 0; i--)
            {
                HeightLevel level = _heightLevels[i];
                if (level.HasTile(position))
                {
                    return level.HeightIndex;
                }
            }

            return 0;
        }
    }
}


