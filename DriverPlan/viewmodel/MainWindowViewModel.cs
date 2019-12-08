using System.ComponentModel;
using System.Runtime.CompilerServices;
using DriverPlan.Annotations;

namespace DriverPlan.viewmodel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {

        public string HelloBinding { get; set; } = "Hello :)";

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string _PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(_PropertyName));
        }
    }
}