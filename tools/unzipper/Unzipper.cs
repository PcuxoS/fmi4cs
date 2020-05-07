using System;
using System.IO;
using System.IO.Compression;


namespace Unzipper

{
    public class SimpleUnzipper : IUnzipper
    {
        public bool Unzip(string zipPath, string extractPath = "Temp")
        {
            ZipFile.ExtractToDirectory(zipPath, extractPath+"\\"+zipPath);
            return true;
        }

        public bool Delete(string path)
        {
            if (Directory.Exists(path))
                Directory.Delete(path, true);
            return true;
        }
    }
}
