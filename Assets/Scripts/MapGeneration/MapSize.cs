using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CuteNewtTest.MapGeneration
{
    [Serializable]
    public struct MapSize
    {
        public int Width;
        public int Height;
        public int Area => Width * Height;

        public void ForEachPosition(Action<Vector3Int> callback)
        {
            for(int x = 0; x < Width;  x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    callback(new(x, y));
                }
            }
           
        }

    }

}