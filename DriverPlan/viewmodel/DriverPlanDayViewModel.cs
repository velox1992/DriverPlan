using System.Collections.Generic;
using System.Linq;

namespace DriverPlan.viewmodel
{
    internal class DriverPlanDayViewModel : BaseViewModel
    {
        private const int cNoStartEndProvidedValue = -1;

        private Dictionary<int, List<DriverPlanEntryViewModel>> FDriverPlanByHour;

        public DriverPlanDayViewModel(List<DriverPlanEntryViewModel> _DriverPlanEntriesOfDay, int _FirstHour, int _LastHour)
        {
            DriverPlanByHour = new Dictionary<int, List<DriverPlanEntryViewModel>>();
            ScheduleDay(_DriverPlanEntriesOfDay, _FirstHour, _LastHour);
        }

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

        private void ScheduleDay(List<DriverPlanEntryViewModel> _DriverPlanEntriesOfDay, int _FirstHour = cNoStartEndProvidedValue, int _LastHour = cNoStartEndProvidedValue)
        {
            var hFirstEntryHour = _FirstHour == cNoStartEndProvidedValue ? _DriverPlanEntriesOfDay.Min(_ => _.DeliveryDate.Hour) : _FirstHour ;
            var hLastEntryHour = _LastHour == cNoStartEndProvidedValue ? _DriverPlanEntriesOfDay.Max(_ => _.DeliveryDate.Hour) : _LastHour;

            DriverPlanByHour.Clear();

            // Erstellen der Struktur
            for (var hHour = hFirstEntryHour; hHour <= hLastEntryHour; hHour++)
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