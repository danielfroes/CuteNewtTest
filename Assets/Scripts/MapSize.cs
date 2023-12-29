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


        public void ForEachPosition(Action<Vector3Int> callback, IterationDirection direction = IterationDirection.Down, bool randomStartingPosition = false)
        {

            Vector3Int startingPoint = randomStartingPosition ? GetRandomPosition() : new Vector3Int(0, 0);

            switch (direction)
            {
                case IterationDirection.Up:
                    InterateBottomToTop(callback, startingPoint);
                    break;
                case IterationDirection.Down:
                    InterateTopToBottom(callback, startingPoint);
                    break;
                case IterationDirection.Left:
                    InterateRightToLeft(callback, startingPoint);
                    break;
                case IterationDirection.Right:
                    InterateLeftToRight(callback, startingPoint);
                    break;
            }
        }


        Vector3Int GetRandomPosition()
        {
            return new (Random.Range(0, Width), Random.Range(0, Height));
        }

        void InterateTopToBottom(Action<Vector3Int> callback, Vector3Int startingPosition)
        {
            for (int x = startingPosition.x; x < Width; x++)
            {
                for (int y = startingPosition.y; y < Height; y++)
                {
                    callback?.Invoke(new(x, y));
                }
            }

            for (int x = 0; x < startingPosition.x; x++)
            {
                for (int y = 0; y < startingPosition.y; y++)
                {

                    callback.Invoke(new(x, y));
                }
            }
        }

        void InterateBottomToTop(Action<Vector3Int> callback, Vector3Int startingPosition)
        {
            for (int x = startingPosition.x; x >= 0 ; x--)
            {
                for (int y = startingPosition.y; y >= 0; y--)
                {
                    callback?.Invoke(new(x, y));
                }
            }

            for (int x = Width - 1; x > startingPosition.x; x--)
            {
                for (int y = Height - 1; y > startingPosition.y; y--)
                {
                    callback.Invoke(new(x, y));
                }
            }
        }

        void InterateLeftToRight(Action<Vector3Int> callback, Vector3Int startingPosition)
        {
            for (int y = startingPosition.y; y < Height; y++)
            {
                for (int x = startingPosition.x; x < Width; x++)
                {
                    callback?.Invoke(new(x, y));
                }
            }

            for (int y = 0; y < startingPosition.y; y++)
            {                
                for (int x = 0; x < startingPosition.x; x++)
                {
                    callback.Invoke(new(x, y));
                }
            }
        }

        void InterateRightToLeft(Action<Vector3Int> callback, Vector3Int startingPosition)
        {
            for (int y = startingPosition.y; y >= 0; y--)
            {
                for (int x = startingPosition.x; x >= 0; x--)
                {
                    callback?.Invoke(new(x, y));
                }
            }

            for (int y = Height - 1; y > startingPosition.y; y--)
            { 
                for (int x = Width - 1; x > startingPosition.x; x--)
                {
                    callback.Invoke(new(x, y));
                }
            }
        }


    }

    public enum IterationDirection
    {
        Up,
        Down,
        Left,
        Right,
    }
}