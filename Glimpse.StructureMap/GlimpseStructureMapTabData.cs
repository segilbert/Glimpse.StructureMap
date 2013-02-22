using System.Collections.Generic;

namespace Glimpse.StructureMap
{
    public class GlimpseStructureMapTabData
    {
        //public List<object[]> FamilyRegistrations = new List<object[]> { new object[] { "PluginType", "Namespace", "Assembly", "Concrete Name", "Concrete Details", "Description", "All Concretes" } };
        public List<object[]> FamilyRegistrations = new List<object[]> { new object[] { "PluginType", "Namespace", "Assembly", "Concrete", "Namespace", "Assembly", "Description"} };
        public List<object[]> ConfigurationSources = new List<object[]> { new object[] { "#", "Registry" } };
        public List<object[]> General = new List<object[]> { new object[] { "Container Name", "Family Count", "Log Error Count", "Current Profile", "Default Profile Name" } };
        public string CurrentProfile { get; set; }
        public string Name { get; set; }
        public string StructureMapVersion { get; set; }
        public int FamilyCount { get; set; }
        public int LogErrorCount { get; set; }
        public string DefaultProfileName { get; set; }
    }
}