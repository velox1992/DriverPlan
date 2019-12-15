using System.Collections.Generic;
using System.Linq;

namespace DriverPlan.viewmodel
{
    internal class DriverPlanDayViewModel : BaseViewModel
    {
        private Dictionary<int, List<DriverPlanEntryViewModel>> FDriverPlanByHour;

        public DriverPlanDayViewModel(List<DriverPlanEntryViewModel> _DriverPlanEntriesOfDay)
        {
            DriverPlanByHour = new Dictionary<int, List<DriverPlanEntryViewModel>>();
            ScheduleDay(_DriverPlanEntriesOfDay);
        }

        public Dictionary<int, List<DriverPlanEntryViewModel>> DriverPlanByHour
        {
            get => FDriverPlanByHour;
            set
            {
                FDriverPlanByHour = value;
                OnPropertyChanged();
            }
        }

        private void ScheduleDay(List<DriverPlanEntryViewModel> _DriverPlanEntriesOfDay)
        {
            var hFirstEntryHour =
                _DriverPlanEntriesOfDay.Min(_ => _.DeliveryDate.Hour);

            var hLatestEntryHour =
                _DriverPlanEntriesOfDay.Max(_ => _.DeliveryDate.Hour);

            DriverPlanByHour.Clear();

            // Erstellen der Struktur
            for (var hHour = hFirstEntryHour; hHour <= hLatestEntryHour; hHour++)
                DriverPlanByHour.Add(hHour, new List<DriverPlanEntryViewModel>());

            // Einsortieren der Fahrten
            foreach (var hDriverPlanEntryViewModel in _DriverPlanEntriesOfDay)
            {
                var hDeliveryDate = hDriverPlanEntryViewModel.DeliveryDate.Hour;
                DriverPlanByHour[hDeliveryDate].Add(hDriverPlanEntryViewModel);
            }

            DriverPlanByHour = DriverPlanByHour;

            OnPropertyChanged(nameof(DriverPlanByHour));
        }
    }
}