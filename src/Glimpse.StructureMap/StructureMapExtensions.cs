﻿//
using System;
//
using StructureMap;

namespace Glimpse.StructureMap
{
    public static class StructureMapExtensions
    {
        public static void ActivateGlimpseStructureMap(this IContainer container)
        {
            if (container == null) throw new ArgumentNullException("container");
            StructureMapPlugin.ContainerInstance = container;
        }
    }
}
