using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RadialMenu.Utility
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Public Methods
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
