using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;

namespace EVEMarket.WPF.ViewModel
{
    public class PageViewModel : ViewModelBase
    {
        private ImageSource _icon;
        private ICommand _command;
        private string _iconPath;
        private string _name;

        /// <summary>
        /// TODO icon path and icon property sync
        /// </summary>
        public ImageSource Icon
        {
            get => _icon;
            set => Set(ref _icon, value);
        }

        /// <summary>
        /// TODO icon path and icon property sync
        /// </summary>
        public string IconPath
        {
            get => _iconPath;
            set => Set(ref _iconPath, value);
        }

        public ICommand Command
        {
            get => _command;
            set => Set(ref _command, value);
        }

        public string Name
        {
            get => _name;
            set => Set(ref _name, value);
        }
    }
}