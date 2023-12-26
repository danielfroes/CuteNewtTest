using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class CompleteLayer : AMapLayer
    {
        public CompleteLayer(MapLayerData data, Transform tilemapParent, MapSize mapSize) : base(data, tilemapParent, mapSize)
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