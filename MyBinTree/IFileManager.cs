using System;
using System.Collections.Generic;
using System.Text;

namespace MyBinTree
{
    public interface IFileManager
    {
        void FileFromData(string filename, string dataFromDict);
        string DataFromFile(string filename);
    }
}
