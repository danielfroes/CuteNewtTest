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
            AMapLayer currentBase = null;
            int sortingOrder = 0;
            foreach (HeightLevelData heightLevel in _heightLevels)
            {
                currentBase = GenerateMap(heightLevel.BaseLayer, currentBase, sortingOrder);
                sortingOrder++;
                foreach (MapLayerData layerData in heightLevel.DetailsLayers)
                {
                    GenerateMap(layerData, currentBase, sortingOrder);
                    sortingOrder++;
                }
            }
        }

        AMapLayer GenerateMap(MapLayerData mapLayerData, AMapLayer baseLayer, int sortingOrder)
        {
            if (!mapLayerData.Active)
                return null;
            
            AMapLayer mapLayer = CreateMapLayer(mapLayerData, baseLayer);
            mapLayer.CreateTilemap(_tilemapParent, sortingOrder);
            mapLayer.GenerateMap();
            _layers.Add(mapLayer);
            return mapLayer;
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

        AMapLayer CreateMapLayer(MapLayerData mapLayerData, AMapLayer baseLayer)
        {
            return (mapLayerData.UseCellularAutomaton, mapLayerData.UseCascade) switch
            {
                (true, false) => new CellularAutomatonLayer(mapLayerData, _size, baseLayer),
                (true, true) => new CascadeMapLayer(mapLayerData,  _size, baseLayer),
                _ => new CompleteLayer(mapLayerData, _size, baseLayer)
            }; ;
        }
    }
}


