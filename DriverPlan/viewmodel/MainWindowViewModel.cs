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
    class MainWindowViewModel : BaseViewModel
    {
        private Dictionary<DateTime, DriverPlanDayViewModel> FAllDriverPlans;
        private ObservableCollection<DriverPlanEntryViewModel> FDriverPlanEntries;

        public MainWindowViewModel()
        {
            FAllDriverPlans = new Dictionary<DateTime, DriverPlanDayViewModel>();
            FDriverPlanEntries = new ObservableCollection<DriverPlanEntryViewModel>();


            InitializeWithTestData();


            CreateNewPlanCommand = new RelayCommand(
                _Parameter =>
                {
                    DataRepository = new DataRepository();
                    DataRepository.DataChanged += DataRepositoryOnDataChanged;
                },
                _Parameter => true);

            LoadPlanCommand = new RelayCommand(
                _Parameter =>
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
                _Parameter => true);

            SavePlanCommand = new RelayCommand(
                _Parameter =>
                {
                    if (DataRepository is null) return;

                    var hSaveFileDialog = new SaveFileDialog();
                    var hDialogResult = hSaveFileDialog.ShowDialog();

                    if (!hDialogResult.GetValueOrDefault()) return;

                    var hFileName = hSaveFileDialog.FileName;

                    var hExporter = new JsonExporter(hFileName);
                    DataRepository.SaveData(hExporter);
                },
                _Parameter => true);

            AddNewItemCommand = new RelayCommand(
                _Parameter =>
                {
                    var hNewDriverInfo = new DriverInfo
                    {
                        Driver = NewItemName,
                        Note = NewItemNote
                    };
                    DataRepository?.AddNewItem(hNewDriverInfo);
                },
                _Parameter => true);
        }

   

        public Dictionary<DateTime, DriverPlanDayViewModel> AllDriverPlans
        {
            get => FAllDriverPlans;
            set
            {
                if (FAllDriverPlans.Equals(value)) return;

                FAllDriverPlans = value;
                OnPropertyChanged();
            }
        }


        // ToDo: Edit auslagern
        public string NewItemName { get; set; }

        public string NewItemNote { get; set; }

        public DateTime NewItemDate { get; set; }

        public RelayCommand SavePlanCommand { get; set; }

        public RelayCommand CreateNewPlanCommand { get; set; }


        public ObservableCollection<DriverPlanEntryViewModel> DriverPlanEntries
        {
            get => FDriverPlanEntries;
            set
            {
                if (FDriverPlanEntries.Equals(value)) return;

                FDriverPlanEntries = value;

                OnPropertyChanged();
            }
        }


        private DataRepository DataRepository { get; set; }

        public RelayCommand LoadPlanCommand { get; }

        public RelayCommand AddNewItemCommand { get; }

        private void InitializeWithTestData()
        {
            DataRepository = new DataRepository();
            DataRepository.DataChanged += DataRepositoryOnDataChanged;

            var hImporter = new TestDataImporter();
            DataRepository.Initialize(hImporter);
        }

        private void DataRepositoryOnDataChanged(object _Sender, EventArgs _E)
        {
            DriverPlanEntries.Clear();

            DataRepository.DriverInfos.ForEach(_ =>
            {
                var hNewDriverPlanEntryViewModel = new DriverPlanEntryViewModel(_);
                DriverPlanEntries.Add(hNewDriverPlanEntryViewModel);
            });

            GenerateDriverPlan();
        }


        private void GenerateDriverPlan()
        {
            AllDriverPlans.Clear();

            var hDriverPlansByDay = new Dictionary<DateTime, List<DriverPlanEntryViewModel>>();

            foreach (var hDriverPlanEntryViewModel in DriverPlanEntries)
            {
                var hDeliveryDate = hDriverPlanEntryViewModel.DeliveryDate.Date;
                if (hDriverPlansByDay.ContainsKey(hDeliveryDate))
                {
                    hDriverPlansByDay[hDeliveryDate].Add(hDriverPlanEntryViewModel);
                }
                else
                {
                    hDriverPlansByDay.Add(hDeliveryDate, new List<DriverPlanEntryViewModel> { hDriverPlanEntryViewModel });
                }
            }

            foreach (var hDriverPlanDay in hDriverPlansByDay)
            {
                AllDriverPlans.Add(hDriverPlanDay.Key, new DriverPlanDayViewModel(hDriverPlanDay.Value));
            }



            OnPropertyChanged(nameof(AllDriverPlans));
        }
    }


    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_PropertyName));
        }
    }
}