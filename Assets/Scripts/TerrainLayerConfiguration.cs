using CuteNewtTest.MapGeneration.Strategies;
using CuteNewtTest.Tools;
using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace CuteNewtTest.MapGeneration
{

    [CreateAssetMenu(fileName = "new Terrain", menuName = "Map Generation/Terrain Configurations")]
    public class TerrainLayerConfiguration : ScriptableObject
    {
        [SerializeField] bool _active = true;
        [SerializeField] TileBase _tile;
        [SerializeReference, SerializeReferenceMenu] IMapGenerationStrategy _strategy;
        [SerializeField] bool _hasWall;
        [ShowIf(nameof(_hasWall)), SerializeField] WallSettings _wallSettings;

        public bool Active => _active;
        public TileBase MainTile => _tile;
        public WallSettings WallSettings => _wallSettings;
        public bool HasWall => _hasWall;
        public IMapGenerationStrategy GenerationStrategy => _strategy;

    }

    [Serializable]
    public class WallSettings
    {
        [SerializeField] TileBase _wall;

        [Range(0, 6), SerializeField] int _height;
        public TileBase Tile => _wall;
        public int Height => _height;
    }

   
}
