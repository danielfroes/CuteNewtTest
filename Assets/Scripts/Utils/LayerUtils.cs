using UnityEngine;

namespace CuteNewtTest.Utils
{
    public static class LayerUtils
    {
        public static LayerMask LayerToMask(int layer) => 1 << layer;
    }
}
