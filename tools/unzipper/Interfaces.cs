using System;
using System.Collections.Generic;
using System.Text;

namespace Unzipper
{
    public interface IUnzipper
    {
        bool Unzip(string zipPath, string extractPath = "Temp");
        bool Delete(string path);
    }
}
