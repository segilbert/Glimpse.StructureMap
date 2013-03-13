//
using System;
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

        public override string Name
        {
            get { return "StructureMap"; }
        }

        public override object GetData(ITabContext context)
        {
            if (ContainerInstance == null) return null;

            return GetGlimpseStructureMapTabData(ContainerInstance);
        }

        public string DocumentationUri
        {
            get { return ""; }
        }
        
        private List<object[]> GetGlimpseStructureMapTabData(IContainer pxContainer)
        {
            if (pxContainer == null) return null;

            // Data Structure
            List<object[]> root = new List<object[]> {new object[] {"Section", "Details"}};
            List<object[]> familyRegistration = new List<object[]> { new object[] { "PluginType", "Concrete" } };
            List<object[]> general = new List<object[]> { new object[] { "Property", "Value"}};
        
            Container container = (Container) pxContainer;
            IEnumerable<InstanceRef> registrations = pxContainer.Model.AllInstances;

            // Create General Data
            general.Add(new object[] { "Name", container.Name });
            general.Add(new object[] { "FamilyCount", container.PluginGraph.FamilyCount });
            general.Add(new object[] { "ErrorCount", container.PluginGraph.Log.ErrorCount });
            general.Add(new object[] { "CurrentProfile", container.PluginGraph.ProfileManager.CurrentProfile });
            general.Add(new object[] { "DefaultProfileName", container.PluginGraph.ProfileManager.DefaultProfileName });
            //
            root.Add(new object[] { "General", general });

            // Configuration Sources
            root.Add(new object[] { "Configuration Sources", GetConfigurationSources(container) });


            // Family Registrations
            familyRegistration.AddRange(registrations.Select(registration => new object[]
                {
                    GetRegistrationPluginType(registration,pxContainer),
                    GetRegistrationConcrete(registration)
                }));
            //
           root.Add(new object[] { "Family Registrations", familyRegistration });

            return root;
        }

        private string FormatLifecycle(IPluginTypeConfiguration pxTypeConfiguration)
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

        private List<object[]> GetRegistrationPluginType(InstanceRef registration, IContainer pxContainer)
        {
            List<object[]> propertyValue = new List<object[]> {new object[] {"Property", "Value"}};

            propertyValue.Add(new object[]{ "PluginType",
                                  String.Format("{0} ({1}) \r\n\r Scoped: {2}",
                                              registration.PluginType.Name,
                                              registration.PluginType.FullName,
                                              FormatLifecycle(pxContainer.Model.For(registration.PluginType))),
                                  (registration.ConcreteType != null ? "info" : "warn")});
            propertyValue.Add(new object[] {"Namespace", registration.PluginType.Namespace });
            propertyValue.Add(new object[] {"Assembly", registration.PluginType.AssemblyQualifiedName});

            return propertyValue;
        }

        private List<object[]> GetRegistrationConcrete(InstanceRef registration)
        {
            List<object[]> propertyValue = new List<object[]> { new object[] { "Property", "Value"}};

            propertyValue.Add(registration.ConcreteType != null
                                  ? new object[]
                                      {
                                          "Concrete",
                                          String.Format("{0} ({1})", registration.ConcreteType.Name,
                                                        registration.ConcreteType.FullName),
                                          "info"
                                      }
                                  : new object[] {"Concrete", string.Empty, "warn"});

            propertyValue.Add(new object[] {"Description",registration.Description});
            propertyValue.Add(new object[] {"Namespace", (registration.ConcreteType != null ? registration.ConcreteType.Namespace : string.Empty)});
            propertyValue.Add(new object[] {"Assembly",(registration.ConcreteType != null ? registration.ConcreteType.AssemblyQualifiedName : string.Empty)});

            return propertyValue;
        }

        private List<object[]> GetConfigurationSources(Container container)
        {
            List<object[]> configurationSources = new List<object[]> { new object[] { "Registry", "Details" } };
            

            for (int i = 0; i < container.PluginGraph.Log.Sources.Length; i++)
            {
                var source = container.PluginGraph.Log.Sources[i];
                var segments = source.Split();

                var segs = new List<object[]> {new object[] {"Property", "Value"}};

                for (int index = 0; index < segments.Length; index++)
                {
                    var segment = segments[index];
                    string sName = string.Empty;
                    string sValue = string.Empty;

                    switch (index)
                    {
                        case 2:
                            sName = "Type";
                            sValue = segment.TrimEnd(",".ToCharArray());
                            break;
                        case 3:
                            sName = "Assembly";
                            sValue = segment.TrimEnd(",".ToCharArray());
                            break;
                        case 4:
                            sName = "Version";
                            sValue = segment.Split("=".ToCharArray())[1].TrimEnd(",".ToCharArray());
                            break;
                        case 5:
                            sName = "Culture";
                            sValue = segment.Split("=".ToCharArray())[1].TrimEnd(",".ToCharArray());
                            break;
                        case 6:
                            sName = "Public Key Token";
                            sValue = segment.Split("=".ToCharArray())[1];
                            break;
                        default:
                            break;
                    }

                    if (index > 1)
                        segs.Add(new object[] {sName, sValue});
                }

                configurationSources.Add(new object[]
                    {
                        segments[2].TrimEnd(",".ToCharArray()),
                        segs
                    });
            }

            return configurationSources;
        }
       
    }

}