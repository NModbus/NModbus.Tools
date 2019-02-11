using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NModbus.Tools.Base.Model;
using System;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;

namespace NModbus.Tools.Base.ViewModel
{
    internal class ConnectionSelectionViewModel : ViewModelBase
    {
        private readonly SavedConnections _model;
        private ConnectionViewModel _selectedConnection;

        public ConnectionSelectionViewModel(SavedConnections model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));

            if (model.Connections == null)
            {
                model.Connections = new Connection[] { };
            }

            foreach(var connection in model.Connections)
            {
                Connections.Add(new ConnectionViewModel(connection));
            }

            SelectedConnection = Connections.FirstOrDefault();

            RefreshPortNames();

            RefreshPortNamesCommand = new RelayCommand(RefreshPortNames);
        }

        public ICommand RefreshPortNamesCommand { get; }

        private void RefreshPortNames()
        {
            var portNames = SerialPort.GetPortNames();

            SerialPortNames.Clear();

            foreach (var portName in portNames)
            {
                SerialPortNames.Add(portName);
            }
        }

        public ObservableCollection<string> SerialPortNames { get; } = new ObservableCollection<string>();

        public ObservableCollection<ConnectionViewModel> Connections { get; } = new ObservableCollection<ConnectionViewModel>();

        public ConnectionViewModel SelectedConnection
        {
            get => _selectedConnection;
            set
            {
                _selectedConnection = value;
                RaisePropertyChanged();
            }
        }

        public SavedConnections ToModel()
        {
            _model.Connections = Connections
                .Select(c => c.ToModel())
                .ToArray();

            return _model;
        }

    }
}
