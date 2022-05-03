using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace DirectxWpf.ViewModel
{
    public class SizeViewModel : ViewModelBase
    {
        // Variables ChunkSize
        private int _x = 0;
        public int X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    RaisePropertyChanged();
                }
            }
        }
        private int _y = 0;
        public int Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    RaisePropertyChanged();
                }
            }
        }
        private int _z = 0;
        public int Z
        {
            get { return _z; }
            set
            {
                if (_z != value)
                {
                    _z = value;
                    RaisePropertyChanged();
                }
            }
        }

        // OK
        private RelayCommand<Window> _ok = null;

        public RelayCommand<Window> OkCommand
        {
            get { return _ok ?? (_ok = new RelayCommand<Window>(Ok)); }
        }

        private void Ok(Window window)
        {
            window.DialogResult = true;
        }

        // Cancel
        private RelayCommand<Window> _cancel = null;

        public RelayCommand<Window> CancelCommand
        {
            get { return _cancel ?? (_cancel = new RelayCommand<Window>(Cancel)); }
        }

        private void Cancel(Window window)
        {
            window.DialogResult = false;
        }
    }
}