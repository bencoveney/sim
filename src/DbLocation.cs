using System;
using System.IO;

namespace Sim
{
  class DbLocation
  {
    public static string Get()
    {
      var dir = GetDirectory();
      Directory.CreateDirectory(dir);

      var filename = GetFile();
      var fullPath = Path.Combine(dir, filename);

      return fullPath;
    }

    private static string GetFile()
    {
      var timestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
      return $"{timestamp}.db";
    }

    private static string GetDirectory()
    {
      // Not platform independent
      return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Sim", "Databases");
    }
  }
}