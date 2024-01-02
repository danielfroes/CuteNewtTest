using UnityEngine.Tilemaps;
using UnityEngine;
using System;

namespace CuteNewtTest.MapGeneration
{
    public abstract class AMapLayer : IDisposable
    {
        public Tilemap Tilemap { get; protected set; }
        public MapSize MapSize { get; }
        public TerrainLayer BaseLayer { get; }
        
        protected abstract TileBase MainTile { get; }
        protected abstract string TilemapName { get; } 

        public AMapLayer(MapSize mapSize, TerrainLayer baseLayer)
        {
            MapSize = mapSize;
            BaseLayer = baseLayer;
        }

        public abstract void Generate(Transform tilemapParent, int sortingOrder);

        protected Tilemap CreateTilemap(Transform tilemapParent, int sortingOrder)
        {
            TilemapRenderer renderer = new GameObject(TilemapName, typeof(TilemapRenderer)).GetComponent<TilemapRenderer>();
            renderer.transform.parent = tilemapParent;
            renderer.sortingOrder = sortingOrder;
            renderer.mode = TilemapRenderer.Mode.Individual;
            renderer.sortOrder = TilemapRenderer.SortOrder.TopLeft;
            return renderer.GetComponent<Tilemap>();
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


        #region Helper Functions

        public bool IsMainTile(Vector3Int position)
        {
            return Tilemap.GetTile(position) == MainTile;
        }

        public bool IsTileEmpty(Vector3Int position)
        {
            return Tilemap.GetTile(position) == null;
        }


        public int GetTileCountInNeighbours(Vector3Int position, bool filterOnlyMainTile = true) => GetTileCountInNeighbours(position, Vector2Int.one, filterOnlyMainTile);

        public int GetTileCountInNeighbours(Vector3Int position, Vector2Int offset, bool filterOnlyMainTile = true)
        {
            int count = 0;

            for (int x = position.x - offset.x; x <= position.x + offset.x; x++)
            {
                for (int y = position.y - offset.y; y <= position.y + offset.y; y++)
                {
                    Vector3Int neighbourPosition = new(x, y);

                    if (neighbourPosition != position &&
                        (filterOnlyMainTile ? IsMainTile(neighbourPosition) : !IsTileEmpty(neighbourPosition)))
                        count++;
                }
            }

            return count;
        }

        public virtual void CreateMainTile(Vector3Int position)
        {
            if (!CheckBaseLayer(position))
                return;

            Tilemap.SetTile(position, MainTile);
        }

        bool CheckBaseLayer(Vector3Int position)
        {
            return BaseLayer == null || (BaseLayer.IsMainTile(position) && BaseLayer.GetTileCountInNeighbours(position) == 8);
        }

        public virtual void RemoveTile(Vector3Int position)
        {
            Tilemap.SetTile(position, null);
        }

        public void ForEachPosition(Action<Vector3Int> callback)
        {
            MapSize.ForEachPosition(callback);
        }

        public float GetFilledRate()
        {
            int tileCount = Tilemap.GetTilesRangeCount(new(0, 0), new(MapSize.Width, MapSize.Height));
            Debug.Log(tileCount / (float)MapSize.Area);
            return tileCount / (float)MapSize.Area;
        }
        #endregion
    }


}
