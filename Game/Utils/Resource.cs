using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace Sim.Utils
{
    class Resource
    {
        public enum Kind
        {
            Data,
            Shader,
            Image
        }

        public static string ReadString(string name, Kind kind)
        {
            try
            {
                using var resourceStream = GetResourceStream(name, kind);
                var reader = new StreamReader(resourceStream);
                return reader.ReadToEnd();
            }
            catch (Exception e)
            {
                var availableResources = String.Join(Environment.NewLine, List().Select(res => $"- {res}"));
                throw new Exception($"Could not load resource {name}. Options are: {Environment.NewLine}{availableResources}", e);
            }
        }

        public static byte[] ReadBytes(string name, Kind kind)
        {
            using var resourceStream = GetResourceStream(name, kind);
            byte[] buffer = new byte[resourceStream.Length];
            resourceStream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        public static Image<Rgba32> ReadImage(string name, Kind kind)
        {
            var bytes = Resource.ReadBytes(name, kind);
            return Image.Load<Rgba32>(bytes);
        }

        private static Stream GetResourceStream(string name, Kind kind)
        {
            var location = GetStreamLocation(name, kind);
            var assembly = typeof(Sim.Utils.Resource).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceStream(location);
        }

        private static string GetStreamLocation(string name, Kind kind)
        {
            return kind switch
            {
                Kind.Data => $"Game.Assets.data.{name}",
                Kind.Shader => $"Game.Assets.shader.{name}",
                Kind.Image => $"Game.Assets.img.{name}",
                _ => throw new Exception("Unknown resource kind"),
            };
        }

        public static IEnumerable<string> List()
        {
            var assembly = typeof(Sim.Utils.Resource).GetTypeInfo().Assembly;
            return assembly.GetManifestResourceNames();
        }
    }
}