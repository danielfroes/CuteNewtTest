using UnityEngine.Tilemaps;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public class TerrainLayer : AMapLayer
    {
        protected TerrainLayerConfiguration Configuration { get; }
        protected override TileBase MainTile => Configuration.MainTile;
        protected override string TilemapName => Configuration.MainTile.name;

        WallSettings WallSettings => Configuration.WallSettings;

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
            GenerateWalls();
        }


        void GenerateWalls()
        {
            if (!Configuration.HasWall) return;

            WallSettings wallSettings = Configuration.WallSettings;

            ForEachPosition(position =>
            {
                Vector3Int lowerNeighbourPosition = new(position.x, position.y - 1);

                if (!IsMainTile(position) || !IsTileEmpty(lowerNeighbourPosition))
                    return;

                CreateWall(position);
            });
        }

        void CreateWall(Vector3Int borderPosition)
        {
            TileBase wallTile = GetWallTile(borderPosition);

            for (int i = 1; i <= WallSettings.Height; i++)
            {
                Tilemap.SetTile(new(borderPosition.x, borderPosition.y - i), wallTile);
            }
        }

        TileBase GetWallTile(Vector3Int borderPosition)
        {
            bool hasLeftNeighbour = IsMainTile(borderPosition + Vector3Int.left);
            bool hasRightNeighbour = IsMainTile(borderPosition + Vector3Int.right);

            return (hasLeftNeighbour, hasRightNeighbour) switch
            {
                (true, false) => WallSettings.RightWall,
                (false, true) => WallSettings.LeftWall,
                _ => WallSettings.MiddleWall,
            };
        }
    }

    


}
