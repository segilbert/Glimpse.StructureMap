//
using System.Collections.Generic;
using System.Linq;
//
using StructureMap;
using StructureMap.Query;
//
using Glimpse.Core.Extensibility;

namespace Glimpse.StructureMap
{
    public class StructureMapPlugin : TabBase, IDocumentation
    {
        public static IContainer ContainerInstance { private get; set; }

        public override string Name { get { return "StructureMap"; } }
        
        public override object GetData(ITabContext context)
        {
            if (ContainerInstance == null) return null;

            IEnumerable<InstanceRef> registrations = ContainerInstance.Model.AllInstances;

            var data = new List<object[]> { new object[] { "PluginType", "Namespace", "Assembly", "Concrete", "Namespace", "Assembly", "Description" } };

            data.AddRange(registrations.Select(registration => new object[]
            {
                registration.PluginType,
                registration.PluginType.Namespace,
                registration.PluginType.Assembly.FullName,
                (registration.ConcreteType != null ? registration.ConcreteType.Name : string.Empty),
                (registration.ConcreteType != null ? registration.ConcreteType.Namespace : string.Empty),
                (registration.ConcreteType != null ? registration.ConcreteType.Assembly.FullName : string.Empty),
                registration.Description
            }));

            return data;
        }

        public string DocumentationUri { get { return ""; } }
    }
}