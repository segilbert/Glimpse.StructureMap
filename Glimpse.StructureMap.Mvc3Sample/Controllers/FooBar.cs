namespace Glimpse.StructureMap.Mvc3Sample.Controllers
{
    public class FooBar : IFooBar
    {
        public string Name { get; set; }
        public int CalcualteCalories()
        {
            return 200;
        }
    }
}