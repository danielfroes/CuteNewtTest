using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;


namespace CuteNewtTest.MapGeneration
{
    public class CellularAutomatonLayer : AMapLayer
    {
        CellularAutomatonSettings _settings;

        public CellularAutomatonLayer(MapLayerData data, Transform tilemapParent, MapSize mapSize) : base(data, tilemapParent, mapSize)
        {
            _settings = data.CellularAutomatonSettings;
        }

        public override void GenerateMap()
        {
            CreateTileRandomly();
            ExecuteCellularAutomaton();
            TrimLeftovers();
            
            if(_settings.FillHoles) FillHoles();          
        }

        void CreateTileRandomly()
        {
            MapSize.ForEachPosition(position =>
            {
                if (CheckForChanceToGenerate())
                {
                    Tilemap.SetTile(position, Data.Tile);
                }
            });

        }

        void ExecuteCellularAutomaton()
        {
            MapSize.ForEachPosition(position =>
            {
                int neigboursCount = GetTileCountInNeighbours(position);

                if (neigboursCount >= _settings.BirthLimit)
                {
                    Tilemap.SetTile(position, Data.Tile);
                }
                else if (neigboursCount <= _settings.DeathLimit)
                {
                    Tilemap.SetTile(position, null);
                }
            });
        }

        protected void TrimLeftovers()
        {
            int clearCount = -1;

            while (clearCount != 0)
            {
                clearCount = 0;

                MapSize.ForEachPosition(position =>
                {
                    if(TryTrimTile(position))
                    {
                        clearCount++;
                    }
                });
            }
        }

        protected bool TryTrimTile(Vector3Int position)
        {
            if (IsMainTile(position) && ValidForTrimming(position))
            {
                Tilemap.SetTile(position, null);
                return true;
            }

            return false;
        }

        bool ValidForTrimming(Vector3Int position)
        {
            return GetTileCountInNeighbours(position) <= _settings.TrimLimit || IsCorridor(position);
        }

        //TODO: Tentar deixar menos feio;
        bool IsCorridor(Vector3Int position)
        {
            bool vertical = IsTileEmpty(new(position.x, position.y - 1)) &&
                            IsTileEmpty(new(position.x, position.y + 1));

            bool horizontal = IsTileEmpty(new(position.x + 1, position.y)) &&
                              IsTileEmpty(new(position.x - 1, position.y));

            bool diagonal = IsTileEmpty(new(position.x + 1, position.y + 1)) &&
                            IsTileEmpty(new(position.x - 1, position.y - 1));

            bool invertedDiagonal = IsTileEmpty(new(position.x + 1, position.y - 1)) &&
                                    IsTileEmpty(new(position.x - 1, position.y + 1));

            return vertical || horizontal || diagonal || invertedDiagonal;
        }

        protected int GetTileCountInNeighbours(Vector3Int position)
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

        void FillHoles()
        {
            MapSize.ForEachPosition(position =>
            {
                if (!IsTileEmpty(position))
                    return;

                int neighbours = GetTileCountInNeighbours(position);

                if (neighbours >= _settings.FillLimit)
                {
                    Tilemap.SetTile(position, Data.Tile);
                }
            });
        }


        bool IsMainTile(Vector3Int position)
        {
            return Tilemap.GetTile(position) == Data.Tile;
        }

        protected bool IsTileEmpty(Vector3Int position)
        {
            return Tilemap.GetTile(position) == null;
        }

        bool CheckForChanceToGenerate()
        {
            return Random.value < _settings.RandomGeneratorChance;
        }


    }
}