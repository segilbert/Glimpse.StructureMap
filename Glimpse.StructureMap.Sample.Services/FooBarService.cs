using Glimpse.StructureMap.Sample.Core.Interfaces;

namespace Glimpse.StructureMap.Sample.Services
{
    public class FooBarService : IFooBarService
    {
        public string Name { get; set; }
        public int CalcualteCalories()
        {
            return 200;
        }
    }
}