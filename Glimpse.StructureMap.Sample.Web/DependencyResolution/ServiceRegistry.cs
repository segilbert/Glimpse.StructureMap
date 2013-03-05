//
//
using Glimpse.StructureMap.Sample.Core.Interfaces;
using Glimpse.StructureMap.Sample.Services;
//
using StructureMap.Configuration.DSL;

namespace Glimpse.StructureMap.Sample.Web.DependencyResolution
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            For<IQuestionService>().Use<QuestionService>()
                .Named("Question service.");

            For<IFooBar2Service>().Use<FooBar2Service>()
                .Named("Foo Bar 2 Service");

            For<IFooBarService>().Use<FooBarService>()
                .Named("Foo Bar Service");

            // Leaving Some Other Interface Service out
            For<ISomeOtherInterface>().Use<SomeOtherInterface>()
               .Named("Some Other Service");
        }
    }

    

    

    
}