//

using System;
using System.Collections.Generic;
//
using System.Linq;
using StructureMap;
//
using Glimpse.Core.Extensibility;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.Query;
using StructureMap.TypeRules;

namespace Glimpse.StructureMap
{
    public class StructureMapPlugin : TabBase, IDocumentation
    {
        public static IContainer ContainerInstance { private get; set; }

        public override string Name
        {
            get { return "StructureMap"; }
        }

        public override object GetData(ITabContext context)
        {
            if (ContainerInstance == null) return null;

            //IEnumerable<InstanceRef> registrations = ContainerInstance.Model.AllInstances;

            GlimpseStructureMapTabData glimpseData = GetGlimpseStructureMapTabData(ContainerInstance);

            var data = new List<object[]>();
            data.Add(new object[] {"", new object[] {}});
            data.Add(new object[] {"Configuration Source", glimpseData.ConfigurationSources});
            data.Add(new object[] {"General", glimpseData.General});
            data.Add(new object[] {"Family Registrations", glimpseData.FamilyRegistrations});

            return data;
        }

        public string DocumentationUri
        {
            get { return ""; }
        }

        public GlimpseStructureMapTabData GetGlimpseStructureMapTabData(IContainer pxContainer)
        {
            if (pxContainer == null) return null;

            Container container = (Container) pxContainer;
            GlimpseStructureMapTabData glimpseData = new GlimpseStructureMapTabData();

            IEnumerable<InstanceRef> registrations = pxContainer.Model.AllInstances;

            // Family Registrations
            glimpseData.FamilyRegistrations.AddRange(registrations.Select(registration => new object[]
                {
                    String.Format("{0} ({1}) \r\n\r Scoped: {2}",
                                  registration.PluginType.Name,
                                  registration.PluginType.FullName,
                                  FormateLifecycle(pxContainer.Model.For(registration.PluginType))),
                    registration.PluginType.Namespace,
                    registration.PluginType.AssemblyQualifiedName,
                    (registration.ConcreteType != null
                         ? String.Format("{0} ({1})", registration.ConcreteType.Name, registration.ConcreteType.FullName)
                         : string.Empty),
                    (registration.ConcreteType != null ? registration.ConcreteType.Namespace : string.Empty),
                    (registration.ConcreteType != null ? registration.ConcreteType.AssemblyQualifiedName : string.Empty)
                    ,
                    registration.Description
                }));

            //string config = pxContainer.WhatDoIHave();
            //System.Diagnostics.Debug.WriteLine(config);

            // General Information
            glimpseData.Name = container.Name;
            glimpseData.FamilyCount = container.PluginGraph.FamilyCount;
            glimpseData.LogErrorCount = container.PluginGraph.Log.ErrorCount;
            glimpseData.CurrentProfile = container.PluginGraph.ProfileManager.CurrentProfile;
            glimpseData.DefaultProfileName = container.PluginGraph.ProfileManager.DefaultProfileName;

            glimpseData.General.Add(new object[]
                {
                    glimpseData.Name,
                    glimpseData.FamilyCount,
                    glimpseData.LogErrorCount,
                    glimpseData.CurrentProfile,
                    glimpseData.DefaultProfileName
                });

            // Configuration Sources
            int i = 1;
            foreach (var source in container.PluginGraph.Log.Sources)
            {
                glimpseData.ConfigurationSources.Add(new object[]
                    {
                        ((string) (object) i.ToString() + (object) ")").PadRight(5),
                        source
                    });
                i++;
            }

            return glimpseData;
        }

        private string FormateLifecycle(IPluginTypeConfiguration pxTypeConfiguration)
        {
            string scope = string.Empty;

            if (pxTypeConfiguration == null)
                return scope;

            if (pxTypeConfiguration.Lifecycle != null)
                scope = "Scoped as:  " + pxTypeConfiguration.Lifecycle;
            else
                scope = "Scoped as:  PerRequest/Transient";

            return scope;
        }
    }

}