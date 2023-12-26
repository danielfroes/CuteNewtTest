using System;
using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    [Serializable]
    public struct MapSize
    {
        public int Width;
        public int Height;


        public void ForEachPosition(Action<Vector3Int> callback)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    callback?.Invoke(new(x, y));
                }
            }
        }
    }
}