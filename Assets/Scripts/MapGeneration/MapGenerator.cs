using Assets.Scripts.Utils;
using NaughtyAttributes;

using System.Collections.Generic;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] Transform _tilemapParent;
        [SerializeField] List<HeightLevelData> _heightLevels = new();
        [SerializeField] MapSize _size;

        List<AMapLayer> _layers = new();

        [Button()]
        void GenerateMap()
        {
            ClearMap();
            GenerateLayers();
        }

        void GenerateLayers()
        {
            int sortingOrder = 0;

            TerrainLayer currentBase = GenerateTerrain(_heightLevels[0].BaseLayer, null, sortingOrder++);

            for (int i = 0; i < _heightLevels.Count; i++)
            {
                HeightLevelData currentLevel = _heightLevels[i];

                foreach (TerrainLayerConfiguration layerData in currentLevel.DetailsLayers)
                {
                    GenerateTerrain(layerData, currentBase, sortingOrder++);
                }

                var nextLevel = _heightLevels.GetElement(i + 1);
                var nextBase = nextLevel != null? GenerateTerrain(nextLevel.BaseLayer, currentBase, sortingOrder) : null;

                GenerateProps(currentLevel.Props, currentBase, nextBase, sortingOrder++);


                currentBase = nextBase;

            }
        }

        private void GenerateProps(IReadOnlyList<PropsConfiguration> props, TerrainLayer baseLayer, TerrainLayer aboveLayer, int sortingOrder)
        {
            PropsLayer layer = new(props, _size, baseLayer, aboveLayer);
            layer.Generate(_tilemapParent, sortingOrder);
            _layers.Add(layer);
        }

        TerrainLayer GenerateTerrain(TerrainLayerConfiguration configuration, TerrainLayer baseLayer, int sortingOrder)
        {
            TerrainLayer layer = new(configuration, _size, baseLayer);
            layer.Generate(_tilemapParent, sortingOrder);
            _layers.Add(layer);
            return layer;
        }

        void ClearMap()
        {
            _layers.ForEach(layer => layer?.Dispose());
            _layers.Clear();

            for (int i = _tilemapParent.childCount - 1; i >= 0; i--)
            {
                GameObject child = _tilemapParent.GetChild(i).gameObject;
                DestroyImmediate(child);
            }
        }
    }
}


