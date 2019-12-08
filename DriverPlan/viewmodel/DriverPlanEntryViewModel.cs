using System;
using System.Collections.Generic;
using System.Text;
using DriverPlan.Annotations;
using DriverPlan.model;

namespace DriverPlan.viewmodel
{
    class DriverPlanEntryViewModel : BaseViewModel
    {
        private DriverInfo FItem;

        public DriverPlanEntryViewModel(DriverInfo _FItem)
        {
            FItem = _FItem;
        }

        public string Driver => FItem.Driver;

        public DateTime DeliveryTime => FItem.DeliveryTime;

        public string DeliveryLocation => FItem.DeliveryLocation;

        public string Note => FItem.Note;
    }
}
