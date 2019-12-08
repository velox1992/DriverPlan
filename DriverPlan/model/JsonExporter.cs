using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Shapes;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Path = System.IO.Path;

namespace DriverPlan.model
{
    internal interface IExporter
    {
        void ExportData(List<DriverInfo> _DriverInfos);
    }

    internal class JsonExporter : IExporter
    {
        public JsonExporter(string _FilePath)
        {
            FilePath = _FilePath;
        }

        public string FilePath { get; }

        public void ExportData(List<DriverInfo> _DriverInfos)
        {
            var hDirectory = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(hDirectory)) return;

            using var hFile = File.CreateText(FilePath);
            var hSerializer = new Newtonsoft.Json.JsonSerializer();
            hSerializer.Serialize(hFile, _DriverInfos);
        }
    }
}