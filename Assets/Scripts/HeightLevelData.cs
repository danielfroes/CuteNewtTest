using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace CuteNewtTest.MapGeneration
{
    [Serializable]
    public class HeightLevelData 
    {
        [SerializeField] MapLayerData _baseLayer;
        [Expandable, SerializeField] List<MapLayerData> _detailsLayers;

        public MapLayerData BaseLayer => _baseLayer;
        public IReadOnlyList<MapLayerData> DetailsLayers => _detailsLayers;
    }
}
