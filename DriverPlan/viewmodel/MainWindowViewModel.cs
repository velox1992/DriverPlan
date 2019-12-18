using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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
        private SortedDictionary<DateTime, DriverPlanDayViewModel> FAllDriverPlans;
        private ObservableCollection<DriverPlanEntryViewModel> FDriverPlanEntries;
        private int FNewItemHour;

        public MainWindowViewModel()
        {
            FAllDriverPlans = new SortedDictionary<DateTime, DriverPlanDayViewModel>();
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
                        DeliveryTime = NewItemDate,
                        DeliveryLocation = NewItemLocation,
                        Driver = NewItemName,
                        Note = NewItemNote
                    };
                    DataRepository?.AddNewItem(hNewDriverInfo);
                },
                _Parameter => true);

            DeleteItemCommand = new RelayCommand(
                _Parameter =>
                {
                    Console.WriteLine("Test");
                }, _Parameter => true);
        }

        public RelayCommand DeleteItemCommand { get; set; }


        public SortedDictionary<DateTime, DriverPlanDayViewModel> AllDriverPlans
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

        public DateTime NewItemDate { get; set; } = DateTime.Today;

        public string NewItemLocation { get; set; }

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

        public int NewItemHour
        {
            get => NewItemDate.Hour;
            set
            {
                NewItemDate = new DateTime(NewItemDate.Year, NewItemDate.Month, NewItemDate.Day, value, NewItemDate.Minute, 0);
            }
        }

        public int NewItemMinute
        {
            get => NewItemDate.Minute;
            set
            {
                NewItemDate = new DateTime(NewItemDate.Year, NewItemDate.Month, NewItemDate.Day, NewItemDate.Hour, value, 0);
            }
        }

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

            AllDriverPlans = GenerateDriverPlan();
        }


        private SortedDictionary<DateTime, DriverPlanDayViewModel> GenerateDriverPlan()
        {
            var hAllDriverPlans = new SortedDictionary<DateTime, DriverPlanDayViewModel>();
            
            var hFirstEntryHour = DriverPlanEntries.Min(_ => _.DeliveryDate.Hour);
            var hLastEntryHour = DriverPlanEntries.Max(_ => _.DeliveryDate.Hour);

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
                hAllDriverPlans.Add(hDriverPlanDay.Key, new DriverPlanDayViewModel(hDriverPlanDay.Value));
            }
            

            return hAllDriverPlans;
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