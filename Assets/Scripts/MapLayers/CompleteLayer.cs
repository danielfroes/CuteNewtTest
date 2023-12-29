using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class CompleteLayer : AMapLayer
    {
        public CompleteLayer(MapLayerData data, MapSize mapSize, AMapLayer baseLayer) : base(data, mapSize, baseLayer)
        {
        }

        public override void GenerateMap()
        {
            MapSize.ForEachPosition(position =>
            {
                Tilemap.SetTile(position, Data.Tile);
            });
        }

    }
}