using UnityEngine;

namespace CuteNewtTest.MapGeneration
{
    public interface IHeightResolver
    {
        public int GetHeightIndexInPosition(Vector3 position);
    }
}
