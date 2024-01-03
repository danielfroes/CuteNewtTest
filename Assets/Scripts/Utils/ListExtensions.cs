using System.Collections.Generic;

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
    }
}
