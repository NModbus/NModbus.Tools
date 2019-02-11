using NModbus.Tools.Base;

namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public interface ISlaveExplorerContext
    {
        byte SlaveAddress { get; }

        IModbusMasterFactory GetModbusMasterFactory();

        IMessageBoxService MessageBoxService { get; }

        void StartPolling(IPoints points);

        bool IsPolling { get; }
    }
}
