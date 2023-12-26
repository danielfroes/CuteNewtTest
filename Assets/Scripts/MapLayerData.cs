using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace CuteNewtTest.MapGeneration
{

    [CreateAssetMenu(fileName = "new Layer Settings", menuName = "Map Generation/Layer Settings")]
    public class MapLayerData : ScriptableObject
    {

        [SerializeField] bool _active = true;
        [SerializeField] TileBase _tile;

        [Header("Sorting Layer Data")]
        [SerializeField, SortingLayer] int _layerId;
        [SerializeField] int _orderInLayer;


        [SerializeField] bool _useCellularAutomaton;
        [ShowIf(nameof(_useCellularAutomaton)), SerializeField] CellularAutomatonSettings _cellularAutomatonSettings;

        [SerializeField] bool _useCascade;
        [ShowIf(nameof(_useCascade)), SerializeField] CascadeSettings _cascadeSettings;


        public bool Active => _active;

        public TileBase Tile => _tile;

        public CellularAutomatonSettings CellularAutomatonSettings => _cellularAutomatonSettings;
        public CascadeSettings CascadeSettings => _cascadeSettings;
        public int LayerId => _layerId;
        public int OrderInLayer => _orderInLayer;
        public bool UseCellularAutomaton => _useCellularAutomaton;
        public bool UseCascade => _useCascade;

    }

    [Serializable]
    public class CellularAutomatonSettings
    {
        [Range(0, 8), SerializeField] int _deathLimit = 2;
        [Range(0, 8), SerializeField] int _birthLimit = 5;
        [Range(0, 3), SerializeField] int _trimLimit = 3;
        [Range(0, 100), SerializeField] int _randomGeneratorChance = 50;
        [SerializeField] bool _fillHoles;
        [Range(0, 8), ShowIf(nameof(_fillHoles)), SerializeField] int _fillLimit = 4;

        public int BirthLimit => _birthLimit;
        public int DeathLimit => _deathLimit;
        public int TrimLimit => _trimLimit;
        public float RandomGeneratorChance => _randomGeneratorChance / 100f;

        public bool FillHoles => _fillHoles;
        public int FillLimit => _fillLimit;
    }


    [Serializable]
    public class CascadeSettings
    {
        [SerializeField] TileBase _cascadeTile;

        [Range(0, 6), SerializeField] int _height;
        public TileBase CascadeTile => _cascadeTile;
        public int Height => _height;
    }
}
