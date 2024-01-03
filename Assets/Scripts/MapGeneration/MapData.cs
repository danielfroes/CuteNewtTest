using System;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    [Serializable]
    public struct MapData
    {
        [SerializeField] MapSize _mapSize;
        [SerializeField] Transform _tilemapParent;

        public MapSize MapSize => _mapSize;
        public Transform TilemapParent => _tilemapParent;

        public MapData(MapSize mapSize, Transform tilemapParent) 
        {
            _mapSize = mapSize;
            _tilemapParent = tilemapParent;
        }
    }
}