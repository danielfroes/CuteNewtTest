using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CuteNewtTest.MapGeneration
{
    public class PropsLayer : AMapLayer
    {
        protected override TileBase MainTile => _activeProps.PropTile;
        protected override string TilemapName => $"{BaseLayer.Tilemap.name}-Props";

        TerrainLayer _aboveLayer;
        PropsLayerConfiguration _configuration;

        PropsData _activeProps;

        public PropsLayer(PropsLayerConfiguration configuration, MapSize mapSize, TerrainLayer baseLayer, TerrainLayer aboveLayer) : base(mapSize, baseLayer)
        {
            _aboveLayer = aboveLayer;
            _configuration = configuration;
        }

        public override void Generate(Transform tilemapParent, int sortingOrder)
        {
            Tilemap = CreateTilemap(tilemapParent, sortingOrder);
            foreach(PropsData props in _configuration.PropsConfigurations)
            {
                _activeProps = props;
                props.Strategy.GenerateMap(this);
            }
        }

        public override void CreateMainTile(Vector3Int position)
        {
            if (!IsTileEmpty(position) || !CheckAboveLayer(position))
                return;

            base.CreateMainTile(position);
           
        }

        bool CheckAboveLayer(Vector3Int position)
        {
            return _aboveLayer == null ||
                (_aboveLayer.IsTileEmpty(position) &&
                _aboveLayer.GetTileCountInNeighbours(position, _activeProps.OffsetToAboveLayerTiles) == 0);
        }

        public override void RemoveTile(Vector3Int position)
        {
            if (!IsTileEmpty(position) && !IsMainTile(position))
                return;

            base.RemoveTile(position);

        }

    }
}
