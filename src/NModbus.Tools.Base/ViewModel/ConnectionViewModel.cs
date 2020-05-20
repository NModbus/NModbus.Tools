using GalaSoft.MvvmLight;
using NModbus.Tools.Base.Model;
using System;
using System.IO.Ports;

namespace NModbus.Tools.Base.ViewModel
{
    internal class ConnectionViewModel : ViewModelBase
    {
        private readonly Connection _model;

        public ConnectionViewModel(Connection model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
        }

        public bool IsTcp => IsInDesignMode || Type == ConnectionType.Tcp;

        public bool IsNetwork => IsInDesignMode || Type == ConnectionType.Tcp || Type == ConnectionType.Udp;

        public bool IsSerial => IsInDesignMode || Type == ConnectionType.Rtu || Type == ConnectionType.Ascii;

        public string Name
        {
            get {  return _model.Name; }
            set
            {
                _model.Name = value;
                RaisePropertyChanged();
            }
        }

        public ConnectionType Type
        {
            get { return _model.Type; }
            set
            {
                _model.Type = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => IsNetwork);
                RaisePropertyChanged(() => IsSerial);
                RaisePropertyChanged(() => IsTcp);
            }
        }

        public string HostName
        {
            get { return _model.HostName; }
            set
            {
                _model.HostName = value;
                RaisePropertyChanged();
            }
        }

        public int Port
        {
            get { return _model.Port; }
            set
            {
                _model.Port = value;
                RaisePropertyChanged();
            }
        }

        public string SerialPortName
        {
            get { return _model.SerialPortName; }
            set
            {
                _model.SerialPortName = value;
                RaisePropertyChanged();
            }
        }

        public int Baud
        {
            get { return _model.Baud; }
            set
            {
                _model.Baud = value;
                RaisePropertyChanged();
            }
        }

        public StopBits StopBits
        {
            get { return _model.StopBits; }
            set
            {
                _model.StopBits = value;
                RaisePropertyChanged();
            }
        }

        public byte DataBits
        {
            get { return _model.DataBits; }
            set
            {
                _model.DataBits = value;
                RaisePropertyChanged();
            }
        }

        public Parity Parity
        {
            get { return _model.Parity; }
            set
            {
                _model.Parity = value;
                RaisePropertyChanged();
            }
        }

        public int ReadTimeout
        {
            get { return _model.ReadTimeout; }
            set
            {
                _model.ReadTimeout = value;
                RaisePropertyChanged();
            }
        }

        public int WriteTimeout
        {
            get { return _model.WriteTimeout; }
            set
            {
                _model.WriteTimeout = value;
                RaisePropertyChanged();
            }
        }

        public int ConnectionTimeout
        {
            get { return _model.ConnectionTimeout; }
            set
            {
                _model.ConnectionTimeout = value;
                RaisePropertyChanged();
            }
        }

        public Connection ToModel()
        {
            return _model.Clone();
        }

    }
}
