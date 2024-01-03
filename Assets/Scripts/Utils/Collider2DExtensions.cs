using UnityEngine;

namespace CuteNewtTest.Utils
{
    public static class Collider2DExtensions
    {
        public static bool IsPlayer(this Collider2D collider)
        {
            return collider.CompareTag("Player");
        }
    }
}
