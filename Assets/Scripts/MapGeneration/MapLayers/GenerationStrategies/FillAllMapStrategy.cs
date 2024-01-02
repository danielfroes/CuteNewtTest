namespace CuteNewtTest.MapGeneration.Strategies
{
    public class FillAllMapStrategy : IMapGenerationStrategy
    {  
        public void GenerateMap(AMapLayer map)
        {
            map.ForEachPosition(position =>
            {
                map.CreateMainTile(position);
            });
        }
    }
}
