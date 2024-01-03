using CuteNewtTest.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace CuteNewtTest.MapGeneration
{
    public class HeightLevel : IDisposable
    {
        HeightLevelConfiguration _configuration;
        MapData _mapData;
        public TerrainLayer Ground { get; private set; }
       
        int HeightSortingOrder => SortingOrderUtils.GetHeightLevelSortingOrder(HeightIndex);
        int NextHeightSortingOrder => SortingOrderUtils.GetHeightLevelSortingOrder(HeightIndex + 1);

        public int HeightIndex { get; private set; }

        List<AMapLayer> _layersInLevel = new(); 

        public HeightLevel(HeightLevelConfiguration levelConfiguration, MapData mapData, int heigthIndex)
        {
            _configuration = levelConfiguration;
            _mapData = mapData;
            HeightIndex = heigthIndex;
        }

        public void Generate(TerrainLayer baseGround)
        {
            CreateGround(baseGround);
            CreateGroundDetails();
        }

        public void CreateProps(TerrainLayer aboveGroundLayer)
        {
            PropsLayer props = new(_configuration.Props, Ground, aboveGroundLayer);
            props.Generate(_mapData, NextHeightSortingOrder);
            _layersInLevel.Add(props);
        }

        void CreateGround(TerrainLayer baseGround)
        {
            Ground = new(_configuration.Ground, baseGround);
            Ground.Generate(_mapData, HeightSortingOrder);
            _mapData = new(_mapData.MapSize, Ground.Tilemap.transform);
            _layersInLevel.Add(Ground);
        }

        void CreateGroundDetails()
        {
            int sortingOrder = HeightSortingOrder + 1;

            foreach (TerrainConfiguration terrainDetailsConfiguration in _configuration.GroundDetails)
            {
                TerrainLayer groundDetail = new(terrainDetailsConfiguration, Ground);
                groundDetail.Generate(_mapData, sortingOrder++);
                _layersInLevel.Add(groundDetail);
            }
        }

        public bool HasTile(Vector3 position)
        {
            Vector3Int tilemapPosition = Vector3Int.RoundToInt(position);
            foreach (AMapLayer layer in _layersInLevel)
            {
                if (layer.Tilemap.HasTile(tilemapPosition))
                    return true;
            }
            return false;
        }

        public void Dispose()
        {
            foreach (AMapLayer layer in _layersInLevel)
            {
                layer.Dispose();
            }
        }
    }
}
