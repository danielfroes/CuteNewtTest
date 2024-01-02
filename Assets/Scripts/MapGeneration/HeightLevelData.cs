using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace CuteNewtTest.MapGeneration
{
    [Serializable]
    public class HeightLevelData 
    {
        [Expandable, SerializeField] TerrainLayerConfiguration _baseLayer;
        [Expandable, SerializeField] List<TerrainLayerConfiguration> _detailsLayers;
        [Expandable, SerializeField] List<PropsConfiguration> _propsConfigurations;

        public TerrainLayerConfiguration BaseLayer => _baseLayer;
        public IReadOnlyList<TerrainLayerConfiguration> DetailsLayers => _detailsLayers;
        public IReadOnlyList<PropsConfiguration> Props => _propsConfigurations;
    }
}
