using System;
using System.Collections.Generic;
using System.Text;
using DriverPlan.Annotations;
using DriverPlan.model;

namespace DriverPlan.viewmodel
{
    class DriverPlanEntryViewModel : BaseViewModel
    {
        private readonly DriverInfo FItem;

        public DriverInfo OriginalItem => FItem;


        public DriverPlanEntryViewModel(DriverInfo _FItem)
        {
            FItem = _FItem;
        }

        public string Driver
        {
            get => FItem.Driver;
            set => FItem.Driver = value;
        }

        public string Note
        {
            get => FItem.Note;
            set => FItem.Note = value;
        }

        public DateTime DeliveryDate
        {
            get => FItem.DeliveryTime;
            set => FItem.DeliveryTime = value;
        }

        public string DeliveryLocation
        {
            get => FItem.DeliveryLocation;
            set => FItem.DeliveryLocation = value;
        }

        public int DeliveryDateDay
        {
            get => DeliveryDate.Day;
            set => DeliveryDate = new DateTime(DeliveryDate.Year, DeliveryDate.Month, value, DeliveryDate.Hour, DeliveryDate.Minute, 0);
        }

        public int DeliveryDateMonth
        {
            get => DeliveryDate.Month;
            set => DeliveryDate = new DateTime(DeliveryDate.Year, value, DeliveryDate.Day, DeliveryDate.Hour, DeliveryDate.Minute, 0);
        }

        public int DeliveryDateYear
        {
            get => DeliveryDate.Year;
            set => DeliveryDate = new DateTime(value, DeliveryDate.Month, DeliveryDate.Day , DeliveryDate.Hour, DeliveryDate.Minute, 0);
        }

        public int DeliveryDateHour
        {
            get => DeliveryDate.Hour;
            set
            {
                if (value < 0 || value > 23)
                {
                    return;
                }

                DeliveryDate = new DateTime(DeliveryDate.Year, DeliveryDate.Month, DeliveryDate.Day, value,
                    DeliveryDate.Minute, 0);
            }
        }

        public int DeliveryDateMinute
        {
            get => DeliveryDate.Minute;
            set
            {
                if (value < 0 || value > 59)
                {
                    return;
                }
                DeliveryDate = new DateTime(DeliveryDate.Year, DeliveryDate.Month, DeliveryDate.Day, DeliveryDate.Hour,
                    value, 0);
            }
        }
    }
}
