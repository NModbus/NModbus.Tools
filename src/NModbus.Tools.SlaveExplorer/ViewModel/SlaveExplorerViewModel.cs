using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Timers;
using System.Windows.Input;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.CommandWpf;
using NModbus.Tools.Base;
using NModbus.Tools.Base.Model;

namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class SlaveExplorerViewModel : ViewModelBase, ISlaveExplorerContext
    {
        private int _errorCount;
        private int _readCount;
        private readonly Timer _pollingTimer;
        private bool _isPollingCancelled;
        private IPoints _pointsToPoll;
        private double _pollingInterval = 2.0;

        private byte _slaveAddress;

        private readonly ConnectionSelectionService _connectionSelectionService = new ConnectionSelectionService();

        private ConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SlaveExplorerViewModel()
        {
            Coils = new DiscretesViewModel(this);
            Inputs = new InputsViewModel(this);
            HoldingRegisters = new HoldingRegistersViewModel(this);
            InputRegisters = new InputRegistersViewModel(this);

            SlaveAddress = 1;

            _pollingTimer = new Timer()
            {
                AutoReset = false
            };

            _pollingTimer.Elapsed += PollingTimerEllapsed;

            StopPollingCommand = new RelayCommand(StopPolling, CanStopPolling);
            SelectConnectionCommand = new RelayCommand(SelectConnection);
        }

        public string Title => "Slave Explorer";

        public ICommand StopPollingCommand { get; }
        public ICommand SelectConnectionCommand { get; }

        private void SelectConnection()
        {
            GetModbusMasterFactoryCore(true);
        }

        private void StopPolling()
        {
            _isPollingCancelled = true;
        }

        private bool CanStopPolling()
        {
            return _pointsToPoll != null && !_isPollingCancelled;
        }

        private void AddLogEntry(string message)
        {
            var now = DateTime.Now;

            var formatted = $"{now.ToShortDateString()} {now.ToShortTimeString()} - {message}";

            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                LogEntries.Add(formatted);
            });
        }

        public int ErrorCount
        {
            get { return _errorCount; }
            private set
            {
                _errorCount = value;
                RaisePropertyChanged();
            }
        }

        public int ReadCount
        {
            get { return _readCount; }
            private set
            {
                _readCount = value;
                RaisePropertyChanged();
            }
        }

        public byte SlaveAddress
        {
            get { return _slaveAddress; }
            set
            {
                _slaveAddress = value;
                RaisePropertyChanged();
            }
        }

        public ConnectionFactory ConnectionFactory
        {
            get { return _connectionFactory; }
            set
            {
                _connectionFactory = value;
                RaisePropertyChanged();
            }
        }

        public ObservableCollection<string> LogEntries { get; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets or sets the number of seconds to wait in between polling attempts.
        /// </summary>
        public double PollingInterval
        {
            get { return _pollingInterval; }
            set
            {
                _pollingInterval = value;
                RaisePropertyChanged();
            }
        }

        public void StartPolling(IPoints points)
        {
            if (_pointsToPoll != null)
                return;

            _pointsToPoll = points ?? throw new ArgumentNullException(nameof(points));
            _isPollingCancelled = false;
            ReadCount = 0;
            ErrorCount = 0;

            _pointsToPoll = points;

            _pollingTimer.Interval = PollingInterval * 1000;
            _pollingTimer.Elapsed += PollingTimerEllapsed;
            _pollingTimer.Enabled = true;
        }

        private void PollingTimerEllapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                if (_isPollingCancelled)
                {
                    _pointsToPoll = null;
                }
                else
                {
                    _pointsToPoll.Read();

                    ReadCount++;
                }
            }
            catch (Exception ex)
            {
                AddLogEntry(ex.Message);

                ErrorCount++;
            }
            finally
            {
                if (!_isPollingCancelled && _pointsToPoll != null)
                {
                    _pollingTimer.Start();
                }
            }
        }

        public IModbusMasterFactory GetModbusMasterFactory()
        {
            return GetModbusMasterFactoryCore(false);
        }

        private IModbusMasterFactory GetModbusMasterFactoryCore(bool force)
        {
            if (ConnectionFactory == null || force)
            {
                var connection = _connectionSelectionService.GetConnection();

                if (connection == null)
                    return null;

                ConnectionFactory = new ConnectionFactory(connection);
            }

            return ConnectionFactory;
        }

        public bool IsPolling
        {
            get { return _pointsToPoll != null; }
        }

        public DiscretesViewModel Coils { get; }

        public InputsViewModel Inputs { get; }

        public HoldingRegistersViewModel HoldingRegisters { get; }

        public InputRegistersViewModel InputRegisters { get; }

        public IMessageBoxService MessageBoxService { get; } = new MessageBoxService();
    }


}
