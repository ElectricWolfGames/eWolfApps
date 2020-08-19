using System.IO;

namespace eWolfCommon.FileIO
{
    public static class StreamFactory
    {
        public static Stream GetStream(string outputName)
        {
            Stream stream = null;
            stream = new FileStream(outputName, FileMode.Create, FileAccess.Write, FileShare.None);

            return stream;
        }
    }
}
