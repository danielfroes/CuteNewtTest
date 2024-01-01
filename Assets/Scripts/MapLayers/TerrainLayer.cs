using UnityEngine.Tilemaps;
using UnityEngine;
using System;

namespace CuteNewtTest.MapGeneration
{
    public class TerrainLayer : AMapLayer
    {
        protected TerrainLayerConfiguration Configuration { get; }
        protected override TileBase MainTile => Configuration.MainTile;
        protected override string TilemapName => Configuration.MainTile.name;

        public TerrainLayer(TerrainLayerConfiguration config, MapSize mapSize, TerrainLayer baseLayer) : base(mapSize, baseLayer)
        {
            Configuration = config;
        }

        public override void Generate(Transform tilemapParent, int sortingOrder)
        {
            if (!Configuration.Active)
                return;

            Tilemap = CreateTilemap(tilemapParent, sortingOrder);
            Configuration.GenerationStrategy.GenerateMap(this);
            CreateWallTiles();
        }


        void CreateWallTiles()
        {
            if (!Configuration.HasWall) return;

            WallSettings wallSettings = Configuration.WallSettings;

            ForEachPosition(position =>
            {
                Vector3Int lowerNeighbourPosition = new(position.x, position.y - 1);

                if (!IsMainTile(position) || !IsTileEmpty(lowerNeighbourPosition))
                    return;

                for (int i = 1; i <= wallSettings.Height; i++)
                {
                    Tilemap.SetTile(new(position.x, position.y - i), wallSettings.Tile);
                }
            });
        }
    }

    


}
