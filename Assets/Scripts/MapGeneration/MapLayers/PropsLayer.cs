using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CuteNewtTest.MapGeneration
{
    public class PropsLayer : AMapLayer
    {
        public override string TilemapName => $"{BaseLayer.TilemapName}-Props";
        protected override TileBase MainTile => _activeProp.PropTile;

        TerrainLayer _aboveLayer;
        IReadOnlyList<PropsConfiguration> _propsConfigurations;

        PropsConfiguration _activeProp;

        public PropsLayer(IReadOnlyList<PropsConfiguration> props, TerrainLayer baseLayer, TerrainLayer aboveLayer) : base(baseLayer)
        {
            _aboveLayer = aboveLayer;
            _propsConfigurations = props;
        }

        protected override void Generate()
        {
            foreach(PropsConfiguration prop in _propsConfigurations)
            {
                _activeProp = prop;
                prop.Strategy.GenerateMap(this);
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
                _aboveLayer.GetTileCountInNeighbours(position, _activeProp.OffsetToAboveLayerTiles, false) == 0);
        }

        public override void RemoveTile(Vector3Int position)
        {
            if (!IsTileEmpty(position) && !IsMainTile(position))
                return;

            base.RemoveTile(position);

        }

    }
}
