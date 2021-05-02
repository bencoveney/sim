using System.IO;
using System.Reflection;

namespace Sim
{
  class Resource
  {
    public static string Read(string name)
    {
      var assembly = typeof(Sim.Resource).GetTypeInfo().Assembly;
      var resource = assembly.GetManifestResourceStream($"sim.data.{name}");
      var reader = new StreamReader(resource);
      return reader.ReadToEnd();
    }
  }
}