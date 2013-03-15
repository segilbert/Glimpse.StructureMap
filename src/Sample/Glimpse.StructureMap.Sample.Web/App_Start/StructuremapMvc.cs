using System.Web.Mvc;
using StructureMap;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Glimpse.StructureMap.Sample.Web.App_Start.StructuremapMvc), "Start")]

namespace Glimpse.StructureMap.Sample.Web.App_Start {
    public static class StructuremapMvc {
        public static void Start() {
            var container = (IContainer) IoC.Initialize();
            container.ActivateGlimpseStructureMap();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
            
        }
    }
}