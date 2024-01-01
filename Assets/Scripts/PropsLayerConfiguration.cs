using CuteNewtTest.MapGeneration.Strategies;
using CuteNewtTest.Tools;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CuteNewtTest.MapGeneration
{
    [CreateAssetMenu(fileName = "new Props", menuName = "Map Generation/Props Configuration")]
    public class PropsLayerConfiguration : ScriptableObject
    {
        [SerializeField] List<PropsData> _propsConfigurations;
        public IReadOnlyList<PropsData> PropsConfigurations => _propsConfigurations;
    }

    [Serializable]
    public class PropsData
    {
        [SerializeField] TileBase _propTile;
        [SerializeField] Vector2Int _offsetToAboveLayerTiles;
        [SerializeReference, SerializeReferenceMenu] IMapGenerationStrategy _strategy;

        public TileBase PropTile => _propTile;
        public IMapGenerationStrategy Strategy => _strategy;
        public Vector2Int OffsetToAboveLayerTiles => _offsetToAboveLayerTiles;
    }
}
