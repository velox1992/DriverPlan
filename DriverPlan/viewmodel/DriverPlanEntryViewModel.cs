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
    }
}
