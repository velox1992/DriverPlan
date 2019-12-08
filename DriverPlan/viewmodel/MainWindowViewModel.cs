using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using DriverPlan.Annotations;
using DriverPlan.Command;
using DriverPlan.model;
using Microsoft.Win32;


namespace DriverPlan.viewmodel
{
    internal class MainWindowViewModel : BaseViewModel 
    {
        private ObservableCollection<DriverPlanEntryViewModel> FDriverPlanEntries;

        // ToDo: Edit auslagern
        public string NewItemName { get; set; }

        public string NewItemNote { get; set; }

        public DateTime NewItemDate { get; set; }

        public MainWindowViewModel()
        {
            FDriverPlanEntries = new ObservableCollection<DriverPlanEntryViewModel>();

            OpenDataSetCommand = new RelayCommand(
                (_Parameter) =>
                {
                    var hOpenFileDialog = new OpenFileDialog();
                    var hDialogResult = hOpenFileDialog.ShowDialog();

                    if (!hDialogResult.GetValueOrDefault()) return;

                    var hFileName = hOpenFileDialog.FileName;

                    DataRepository = new DataRepository();
                    DataRepository.DataChanged += DataRepositoryOnDataChanged;

                    var hImporter = new JsonImporter(hFileName);
                    DataRepository.Initialize(hImporter);
                },
                _Parameter => true );

            AddNewItemCommand = new RelayCommand(
                (_Parameter) =>
                {
                    var hNewDriverInfo = new DriverInfo()
                    {
                        Driver = NewItemName,
                        Note = NewItemNote
                    };
                   DataRepository?.AddNewItem(hNewDriverInfo);
                },
                _Parameter => true);
        }

        private void DataRepositoryOnDataChanged(object _Sender, EventArgs _E)
        {
            DriverPlanEntries.Clear();

            DataRepository.DriverInfos.ForEach( _ =>
            {
                var hNewDriverPlanEntryViewModel =  new DriverPlanEntryViewModel(_);
                DriverPlanEntries.Add(hNewDriverPlanEntryViewModel);
            });

            
        }


        public ObservableCollection<DriverPlanEntryViewModel> DriverPlanEntries
        {
            get => FDriverPlanEntries;
            set
            {
                if (FDriverPlanEntries.Equals(value))
                {
                    return;
                }

                FDriverPlanEntries = value;

                OnPropertyChanged();
            }
        }

        private DataRepository DataRepository { get; set; }

        public RelayCommand OpenDataSetCommand { get; }

        public RelayCommand AddNewItemCommand { get; }
    }

    internal class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_PropertyName));
        }

    }
}

