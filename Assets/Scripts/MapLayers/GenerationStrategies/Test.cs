using UnityEngine;
using NaughtyAttributes;
using System;


namespace CuteNewtTest.MapGeneration.Strategies
{
    public class Test : IMapGenerationStrategy
    {

        [SerializeField, OnValueChanged(nameof(UpdateSettings)), DisableIf(nameof(_showRawSettings)), AllowNesting, Range(1, 10)]
        int _clustersSize = 5;

        [SerializeField, OnValueChanged(nameof(UpdateSettings)), DisableIf(nameof(_showRawSettings)), AllowNesting, Range(1, 3)]
        int _clustersFrequency = 1;

        [SerializeField, OnValueChanged(nameof(UpdateSettings)), DisableIf(nameof(_showRawSettings)), AllowNesting]
        bool _fillHoles = true;

        [SerializeField] bool _showRawSettings;
        [SerializeField, ShowIf(nameof(_showRawSettings)), AllowNesting] CellularAutomatonSettings _settings;



        public void GenerateMap(AMapLayer map)
        {
            CreateTileRandomly(map);
            //ExecuteCellularAutomaton(map);
            //TrimLeftovers(map);
            //FillHoles(map);
        }

        void CreateTileRandomly(AMapLayer map)
        {
            map.ForEachPosition(position =>
            {
                if (CheckForChanceToGenerate(map))
                {
                    map.CreateMainTile(position);
                }
            });
        }

        void ExecuteCellularAutomaton(AMapLayer map)
        {
            map.ForEachPosition(position =>
            {
                int neigboursCount = map.GetTileCountInNeighbours(position);

                if (neigboursCount >= _settings.BirthThreshold)
                {
                    map.CreateMainTile(position);
                }
                else if (neigboursCount <= _settings.DeathThreshold)
                {
                    map.RemoveTile(position);
                }
            });
        }

        void TrimLeftovers(AMapLayer map)
        {
            int clearCount = -1;

            while (clearCount != 0)
            {
                clearCount = 0;
                map.ForEachPosition(position =>
                {
                    clearCount += TryTrimTile(map, position) ? 1 : 0;
                });
            }
        }

        bool TryTrimTile(AMapLayer map, Vector3Int position)
        {
            if (map.IsMainTile(position) && ValidForTrimming(map, position))
            {
                map.RemoveTile(position);
                return true;
            }

            return false;
        }

        bool ValidForTrimming(AMapLayer map, Vector3Int position)
        {
            return map.GetTileCountInNeighbours(position) <= _settings.TrimThreshold || IsCorridor(map, position);
        }

        //TODO: Tentar deixar menos feio;
        bool IsCorridor(AMapLayer map, Vector3Int position)
        {
            bool vertical = map.IsTileEmpty(new(position.x, position.y - 1)) &&
                            map.IsTileEmpty(new(position.x, position.y + 1));

            bool horizontal = map.IsTileEmpty(new(position.x + 1, position.y)) &&
                              map.IsTileEmpty(new(position.x - 1, position.y));

            bool diagonal = map.IsTileEmpty(new(position.x + 1, position.y + 1)) &&
                            map.IsTileEmpty(new(position.x - 1, position.y - 1));

            bool invertedDiagonal = map.IsTileEmpty(new(position.x + 1, position.y - 1)) &&
                                    map.IsTileEmpty(new(position.x - 1, position.y + 1));

            return vertical || horizontal || diagonal || invertedDiagonal;
        }



        void FillHoles(AMapLayer map)
        {
            if (!_settings.FillHoles)
                return;

            map.ForEachPosition(position =>
            {
                if (!map.IsTileEmpty(position))
                    return;

                int neighbours = map.GetTileCountInNeighbours(position);

                if (neighbours >= _settings.FillThreshold)
                {
                    map.CreateMainTile(position);
                }
            });
        }

        bool CheckForChanceToGenerate(AMapLayer map)
        {
            return UnityEngine.Random.value < _settings.RandomGeneratorChance;

        }


        void UpdateSettings()
        {
            _settings.FillHoles = _fillHoles;
            _settings.RandomGeneratorChance = _fillHoles ?
                Mathf.Lerp(0.30f, 0.50f, _clustersSize / 10f) :
                Mathf.Lerp(0.25f, 0.60f, _clustersSize / 10f);

            _settings.DeathThreshold = 3 - _clustersFrequency;
            _settings.TrimThreshold = 3;
            _settings.BirthThreshold = 5;
            _settings.FillThreshold = 4;

        }

        [Serializable]
        class CellularAutomatonSettings
        {
            [Range(0, 8)] public int DeathThreshold = 2;
            [Range(0, 8)] public int BirthThreshold = 5;
            [Range(0, 3)] public int TrimThreshold = 3;
            [Range(0, 1f)] public float RandomGeneratorChance = 0.5f;
            [SerializeField] public bool FillHoles = false;
            [ShowIf(nameof(FillHoles)), AllowNesting, Range(0, 8)] public int FillThreshold = 3;

        }

    }



}