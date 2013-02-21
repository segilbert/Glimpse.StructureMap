using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Glimpse.StructureMap.Mvc3Sample.App_Start.StructuremapMvc), "Start")]

namespace Glimpse.StructureMap.Mvc3Sample.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            container.ActivateGlimpse();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
            
        }
    }
}