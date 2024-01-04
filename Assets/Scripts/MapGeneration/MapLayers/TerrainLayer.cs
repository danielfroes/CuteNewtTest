using UnityEngine.Tilemaps;
using UnityEngine;
using CuteNewtTest.Utils;
using System;

namespace CuteNewtTest.MapGeneration
{
    public class TerrainLayer : AMapLayer
    {
        public override string TilemapName => _configuration.MainTile.name;
        protected override TileBase MainTile => _configuration.MainTile;
        
        WallSettings WallSettings => _configuration.WallSettings;

        TerrainConfiguration _configuration;

        public TerrainLayer(TerrainConfiguration configuration, TerrainLayer baseLayer) : base( baseLayer)
        {
            _configuration = configuration;
        }

        protected override void Generate()
        {
            _configuration.GenerationStrategy.GenerateMap(this);
            GenerateWalls();
        }

        protected override Tilemap CreateTilemap(Transform tilemapParent, int sortingOrder)
        {
            Tilemap tilemap = base.CreateTilemap(tilemapParent, sortingOrder);

            if (_configuration.HasWall)
                tilemap.gameObject.layer = Constants.WALL_LAYER;

            return tilemap;
        }



        public override void CreateMainTile(Vector3Int position)
        {
            if (_configuration.HasWall && !CheckForWallSpace(position))
                return;

            base.CreateMainTile(position);
        }

        bool CheckForWallSpace(Vector3Int position)
        {
            for (int i = 1; i <= WallSettings.Height + 1; i++)
            {
                if(!BaseLayer.IsMainTile(new(position.x, position.y - i)))
                    return false;
            }

            return true;
        }

        void GenerateWalls()
        {
            if (!_configuration.HasWall) return;

            ForEachPosition(position =>
            {
                if (!IsLowerBorder(position))
                    return;

                CreateWall(position);
            });
        }

        bool IsLowerBorder(Vector3Int position)
        {
            Vector3Int lowerNeighbourPosition = new(position.x, position.y - 1);
            return IsMainTile(position) && IsTileEmpty(lowerNeighbourPosition);
        }

        void CreateWall(Vector3Int borderPosition)
        {
            TileBase wallTile = ChooseWallTile(borderPosition);

            for (int i = 1; i <= WallSettings.Height; i++)
            {
                Tilemap.SetTile(new(borderPosition.x, borderPosition.y - i), wallTile);
            }
        }

        TileBase ChooseWallTile(Vector3Int borderPosition)
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
