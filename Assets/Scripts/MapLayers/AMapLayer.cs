using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace CuteNewtTest.MapGeneration
{
    public abstract class AMapLayer : IDisposable
    {
        protected MapLayerData Data { get; }
        protected Tilemap Tilemap { get; private set; }
        protected MapSize MapSize { get; }
        protected AMapLayer BaseLayer { get; }

        public AMapLayer(MapLayerData data, MapSize mapSize, AMapLayer baseLayer)
        {
            Data = data;
            MapSize = mapSize;
            BaseLayer = baseLayer;
        }

        public abstract void GenerateMap();

        public void CreateTilemap(Transform tilemapParent, int sortingOrder)
        {
            TilemapRenderer renderer = new GameObject(Data.Tile.name, typeof(TilemapRenderer)).GetComponent<TilemapRenderer>();
            renderer.transform.parent = tilemapParent;
            //renderer.sortingLayerID = Data.LayerId;
            renderer.sortingOrder = sortingOrder;
            Tilemap = renderer.GetComponent<Tilemap>();
        }


        public bool IsMainTile(Vector3Int position)
        {
            return Tilemap.GetTile(position) == Data.Tile;
        }

        public bool IsTileEmpty(Vector3Int position)
        {
            return Tilemap.GetTile(position) == null;
        }

        public int GetTileCountInNeighbours(Vector3Int position)
        {
            int count = 0;

            for (int x = position.x - 1; x <= position.x + 1; x++)
            {
                for (int y = position.y - 1; y <= position.y + 1; y++)
                {
                    Vector3Int neighbourPosition = new(x, y);

                    if (neighbourPosition != position && IsMainTile(neighbourPosition))
                        count++;
                }
            }

            return count;
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
