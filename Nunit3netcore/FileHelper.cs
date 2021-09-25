using System.IO;

namespace Nunit3netcore
{
    public static class FileHelper
    {
        private static object lockObject = new object();

        public static void CreateFile(string filePath)
        {
            lock (lockObject)
            {
                if (!File.Exists(filePath))
                {
                    using var stream = File.Create(filePath);
                }
            }
        }
    }
}
