using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Sim.Utils
{
    class Resource
    {
        public enum Kind
        {
            Data,
            Shader
        }

        public static string Read(string name, Kind kind)
        {
            return ReadFromAssembly(GetStreamLocation(name, kind));
        }

        public static IEnumerable<string> List()
        {
            var assembly = typeof(Sim.Utils.Resource).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceNames();
        }

        private static string GetStreamLocation(string name, Kind type)
        {
            switch (type)
            {
                case Kind.Data:
                    return $"sim.data.{name}";
                case Kind.Shader:
                    return $"sim.shader.{name}";
                default:
                    throw new Exception("Unknown resource kind");
            }
        }

        private static string ReadFromAssembly(string fullName)
        {
            var assembly = typeof(Sim.Utils.Resource).GetTypeInfo().Assembly;
            var resource = assembly.GetManifestResourceStream(fullName);
            var reader = new StreamReader(resource);
            return reader.ReadToEnd();
        }
    }
}