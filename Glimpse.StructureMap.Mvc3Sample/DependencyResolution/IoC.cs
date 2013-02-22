//
using StructureMap;

namespace Glimpse.StructureMap.Mvc3Sample {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                                    {
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
                                        scan.LookForRegistries();
                                    });
            //                x.For<IExample>().Use<Example>();
                        });
            
            return ObjectFactory.Container;
        }
    }

}