using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoenixSample.PCL
{
    public interface IFileReader
    {
        string[] GetFileContents(string Path);
    }
}
