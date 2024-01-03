namespace CuteNewtTest.Utils
{
    public static class SortingOrderUtils
    {
        const int HEIGHT_LEVEL_SORTING_ORDER_OFSSET = 1000;

        public static int GetHeightLevelSortingOrder(int height)
        {
            return HEIGHT_LEVEL_SORTING_ORDER_OFSSET * height;
        }
    }
}
