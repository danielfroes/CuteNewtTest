using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CuteNewtTest.MapGeneration
{
    public abstract class AMapLayer : IDisposable
    {
        protected MapLayerData Data { get; private set; }
        protected Tilemap Tilemap { get; private set; }
        protected MapSize MapSize { get; private set; }

        public AMapLayer(MapLayerData data, Transform tilemapParent, MapSize mapSize)
        {
            Data = data;
            MapSize = mapSize;
            CreateTilemap(tilemapParent);
        }

        public abstract void GenerateMap();

        void CreateTilemap(Transform tilemapParent)
        {
            TilemapRenderer renderer = new GameObject(Data.Tile.name, typeof(TilemapRenderer)).GetComponent<TilemapRenderer>();
            renderer.transform.parent = tilemapParent;
            renderer.sortingLayerID = Data.LayerId;
            renderer.sortingOrder = Data.OrderInLayer;
            Tilemap = renderer.GetComponent<Tilemap>();
        }

        public void Clear()
        {
            if (Tilemap) Tilemap.ClearAllTiles();
        }

        public void Dispose()
        {
            Clear();

            if (Tilemap) UnityEngine.Object.DestroyImmediate(Tilemap.gameObject);
        }
    }
}
