using System.IO;
using System.Reflection;

namespace Sim.Utils
{
  class Resource
  {
    public static string Read(string name)
    {
      var assembly = typeof(Sim.Utils.Resource).GetTypeInfo().Assembly;
      var resource = assembly.GetManifestResourceStream($"sim.data.{name}");
      var reader = new StreamReader(resource);
      return reader.ReadToEnd();
    }
  }
}