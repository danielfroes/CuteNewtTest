using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace CuteNewtTest.MapGeneration
{
    [Serializable]
    public class HeightLevelConfiguration 
    {
        [Expandable, SerializeField] TerrainConfiguration _ground;
        [Expandable, SerializeField] List<TerrainConfiguration> _groundDetails;
        [Expandable, SerializeField] List<PropsConfiguration> _props;

        public TerrainConfiguration Ground => _ground;
        public IReadOnlyList<TerrainConfiguration> GroundDetails => _groundDetails;
        public IReadOnlyList<PropsConfiguration> Props => _props;


        
    }
}
