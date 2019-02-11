using System;
using NModbus;

namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public class InputRegistersViewModel : PointsViewModelBase<RegisterViewModel, ushort>
    {
        public InputRegistersViewModel(ISlaveExplorerContext context) : base(context)
        {
        }

        protected override ushort[] ReadCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadInputRegisters(slaveId, startAddress, numberOfPoints);
        }

        protected override void WriteCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, ushort[] values)
        {
            throw new NotSupportedException();
        }

        public override bool IsWriteable
        {
            get { return false; }
        }

        public override bool SupportsBlockSize
        {
            get { return true; }
        }
    }
}