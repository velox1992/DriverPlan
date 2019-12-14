using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using DriverPlan.Annotations;

namespace DriverPlan.model
{

    class DriverInfo : INotifyPropertyChanged
    {
        private string FDriver;

        private DateTime FDeliveryTime;

        private string FDeliveryLocation;

        private string FNote;

        // ToDo: Internal Key

        public string Driver
        {
            get => FDriver;
            set
            {
                FDriver = value;
                OnPropertyChanged();
                
            }
        }

        public DateTime DeliveryTime
        {
            get => FDeliveryTime;
            set
            {
                FDeliveryTime = value;
                OnPropertyChanged();
            }
        }

        public string DeliveryLocation
        {
            get => FDeliveryLocation;
            set
            {
                FDeliveryLocation = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get => FNote;
            set
            {
                FNote = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_PropertyName));
        }
    }
}
