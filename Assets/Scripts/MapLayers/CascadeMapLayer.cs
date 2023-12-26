
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class CascadeMapLayer : CellularAutomatonLayer
    {
        CascadeSettings _settings;
        public CascadeMapLayer(MapLayerData data, Transform tilemapParent, MapSize mapSize) : base(data, tilemapParent, mapSize)
        {
             _settings = data.CascadeSettings;
        }

        public override void GenerateMap()
        {
            base.GenerateMap();
            CreateCascadeTiles();
        }

        void CreateCascadeTiles()
        {
            MapSize.ForEachPosition(position =>
            {
                Vector3Int lowerNeighbourPosition = new(position.x, position.y - 1);

                if (Tilemap.GetTile(position) == Data.Tile && IsTileEmpty(lowerNeighbourPosition))
                {
                    for (int i = 1; i <= _settings.Height; i++)
                    {
                        Tilemap.SetTile(new(position.x, position.y - i), _settings.CascadeTile);
                    }

                    TryTrimTile(position + Vector3Int.down * (_settings.Height + 1));
                }
            });
        }
    }
}