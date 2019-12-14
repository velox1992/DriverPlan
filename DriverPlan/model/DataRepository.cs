using System;
using System.Collections.Generic;
using System.Text;

namespace DriverPlan.model
{
    class DataRepository
    {
        public DataRepository()
        {
            DriverInfos = new List<DriverInfo>();
        }

        public List<DriverInfo> DriverInfos { get; private set; }

        public event EventHandler DataChanged;

        public void Initialize(IImporter _Importer)
        {
            if (!_Importer.IsValid()) return;

            DriverInfos = _Importer.GetData();
            DriverInfos.ForEach(_ => _.PropertyChanged += OnItemChanged);

            OnDataChanged();
        }

        private void OnItemChanged(object? _Sender, EventArgs _E)
        {
            OnDataChanged();
        }

        public void SaveData(IExporter _Exporter)
        {
            _Exporter.ExportData(DriverInfos);
        }
        
        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public void AddNewItem(DriverInfo _DriverInfo)
        {
            _DriverInfo.PropertyChanged += OnItemChanged;
            DriverInfos.Add(_DriverInfo);
            OnDataChanged();
            
        }
    }
}
