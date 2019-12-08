using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DriverPlan.model
{
    internal interface IImporter
    {
        List<DriverInfo> GetData();
    }

    internal class JsonImporter : IImporter
    {
        public JsonImporter(string _FilePath)
        {
            FilePath = _FilePath;
        }

        public string FilePath { get; }

        public List<DriverInfo> GetData()
        {
            if (!File.Exists(FilePath)) return null;

            using var hFile = File.OpenText(FilePath);
            var hSerializer = new Newtonsoft.Json.JsonSerializer();
            return (List<DriverInfo>)hSerializer.Deserialize(hFile, typeof(List<DriverInfo>));
        }
    }
}
