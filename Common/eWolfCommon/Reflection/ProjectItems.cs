using System.IO;
using System.Reflection;

namespace eWolfCommon.Reflection
{
    public static class ProjectItems
    {
        public static string LoadFile(string projectFilename)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(projectFilename))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}