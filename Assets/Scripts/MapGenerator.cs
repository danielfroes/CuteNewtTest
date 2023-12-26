using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] Transform _tilemapParent;
        [Expandable, SerializeField] List<MapLayerData> _layersSettings = new();
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
            foreach (MapLayerData layerData in _layersSettings)
            {
                if (!layerData.Active)
                    continue;

                AMapLayer mapLayer = CreateMapLayer(layerData);
                mapLayer.GenerateMap();
                _layers.Add(mapLayer);
            }
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

        AMapLayer CreateMapLayer(MapLayerData data)
        {
            return (data.UseCellularAutomaton, data.UseCascade) switch
            {
                (true, false) => new CellularAutomatonLayer(data, _tilemapParent, _size),
                (true, true) => new CascadeMapLayer(data, _tilemapParent, _size),
                _ => new CompleteLayer(data, _tilemapParent, _size)
            }; ;
        }
    }
}


