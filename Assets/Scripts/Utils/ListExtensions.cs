using System.Collections.Generic;
using UnityEngine;

namespace CuteNewtTest.Utils
{
    public static class ListExtensions
    {
        public static T GetElementOrDefault<T>(this IReadOnlyList<T> list, int index)
        {
            bool indexInRange = index >= 0 && index < list.Count;

            return indexInRange ? list[index] : default;
        }


        public static bool TryGetElement<T>(this IReadOnlyList<T> list, int index, out T result)
        {
            bool indexInRange = index >= 0 && index < list.Count;

            result = indexInRange? list[index] : default;

            return indexInRange;

        }

        public static T GetRandom<T>(this IReadOnlyList<T> list)
        {
            if(list.Count == 0) return default;
            int index = Random.Range(0, list.Count);
            return list[index];
        }

        public static bool IsNullOrEmpty<T>(this IReadOnlyList<T> list)
        {
            return list == null || list.Count == 0;
        }
    }
}
