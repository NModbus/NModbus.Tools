using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using NModbus.Tools.Base.Behaviors;
using NModbus.Tools.Base.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Input;

namespace NModbus.Tools.Base.ViewModel
{
    internal class ConnectionSelectionViewModel : ViewModelBase, ICloseableViewModel
    {
        private readonly SavedConnections _model;
        private ConnectionViewModel _selectedConnection;

        public event EventHandler<CloseEventArgs> Close;

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

            OkCommand = new RelayCommand(Ok, CanOk);
            AddConnectionCommand = new RelayCommand(AddConnection);
            DeleteConnectionCommand = new RelayCommand(DeleteConnection);
            RefreshPortNamesCommand = new RelayCommand(RefreshPortNames);
        }

        public KeyValuePair<string, ConnectionType>[] ConnectionTypes { get; } = new []
        {
            new KeyValuePair<string, ConnectionType>("TCP", ConnectionType.Tcp),
            new KeyValuePair<string, ConnectionType>("UDP", ConnectionType.Udp),
            new KeyValuePair<string, ConnectionType>("RTU", ConnectionType.Rtu),
            new KeyValuePair<string, ConnectionType>("ASCII", ConnectionType.Ascii),
        };

        public KeyValuePair<string, Parity>[] Parities { get; } = new []
        {
            new KeyValuePair<string, Parity>("None", Parity.None),
            new KeyValuePair<string, Parity>("Odd", Parity.Odd),
            new KeyValuePair<string, Parity>("Even", Parity.Even),
            new KeyValuePair<string, Parity>("Mark", Parity.Mark),
            new KeyValuePair<string, Parity>("Space", Parity.Space),
        };

        public KeyValuePair<string, StopBits>[] StopBitses { get; } = new []
        {
            new KeyValuePair<string, StopBits>("None", StopBits.None),
            new KeyValuePair<string, StopBits>("1", StopBits.One),
            new KeyValuePair<string, StopBits>("1.5", StopBits.OnePointFive),
            new KeyValuePair<string, StopBits>("2", StopBits.Two),
        };

        public ICommand OkCommand { get; }
        public ICommand AddConnectionCommand { get; }
        public ICommand DeleteConnectionCommand { get; }
        public ICommand RefreshPortNamesCommand { get; }

        private void Ok()
        {
            //TODO: Close the dialog
            Close?.Invoke(this, new CloseEventArgs(true));
        }

        private bool CanOk()
        {
            if (SelectedConnection == null)
                return false;

            return true;
        }

        private void AddConnection()
        {
            var connection = new Connection
            {
                Name = "New Connection",
                Baud = 19200,
                DataBits = 8,
                Parity = Parity.Even,
                StopBits = StopBits.One,
                ReadTimeout = 2000,
                WriteTimeout = 2000,
                Port = 502,
                Type = ConnectionType.Tcp
            };

            var viewModel = new ConnectionViewModel(connection);

            Connections.Add(viewModel);

            SelectedConnection = viewModel;
        }

        private void DeleteConnection()
        {
            var selectedConnection = SelectedConnection;

            if (selectedConnection == null)
                return;

            Connections.Remove(selectedConnection);

            SelectedConnection = Connections.FirstOrDefault();
        }

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

        public bool CanClose()
        {
            return true;
        }

        public void Closed()
        {
        }
    }
}
