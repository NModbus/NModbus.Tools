using System;
using NModbus;

namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public class InputsViewModel : PointsViewModelBase<DiscreteViewModel, bool>
    {
        public InputsViewModel(ISlaveExplorerContext context) 
            : base(context)
        {
        }

        protected override bool[] ReadCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadInputs(slaveId, startAddress, numberOfPoints);
        }

        protected override void WriteCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, bool[] values)
        {
            throw new NotSupportedException();
        }

        public override bool IsWriteable
        {
            get { return false; }
        }

        public override bool SupportsBlockSize
        {
            get { return false; }
        }
    }
}