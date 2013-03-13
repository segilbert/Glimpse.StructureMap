//

using Glimpse.StructureMap.Sample.Web.DependencyResolution;
using StructureMap;

namespace Glimpse.StructureMap.Sample.Web {
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
                            x.AddRegistry(new ServiceRegistry());
                        });
            
            return ObjectFactory.Container;
        }
    }

}