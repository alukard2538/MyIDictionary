using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MyBinTree
{
    public class FileManager : IFileManager
    {
        public void FileFromData(string filename, string dataFromDict)
        {
            using (var writer = new StreamWriter(filename, false))
            {
                writer.Write(dataFromDict);
            }
        }

        public string DataFromFile(string filename)
        {
            char[] charsToTrim = { ';', '\r', '\n'};
            string data = "";
            using (var reader = new StreamReader(filename))
            {
                data = reader.ReadToEnd();
            }
            data.TrimEnd(charsToTrim);
            return data;
        }
    }
}
