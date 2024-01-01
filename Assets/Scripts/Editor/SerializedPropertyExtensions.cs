using System.Collections.Generic;
using UnityEditor;

namespace CuteNewtTest.Tools
{
    public static class SerializedPropertyExtensions
    {
        public static IEnumerable<SerializedProperty> Children(this SerializedProperty property)
        {
            SerializedProperty last = property.GetEndProperty();
            SerializedProperty current = property.Copy();

            for (int i = 0; current.NextVisible(i == 0) && !SerializedProperty.EqualContents(current, last); i++)
            {
                yield return current;
            }
        }
    }
}