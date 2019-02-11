namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public class DiscretesViewModel : PointsViewModelBase<DiscreteViewModel, bool>
    {
        public DiscretesViewModel(ISlaveExplorerContext context) 
            : base(context)
        {
        }

        protected override bool[] ReadCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadCoils(slaveId, startAddress, numberOfPoints);
        }

        protected override void WriteCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, bool[] values)
        {
            modbusMaster.WriteMultipleCoils(slaveId, startAddress, values);
        }

        public override bool IsWriteable
        {
            get { return true; }
        }

        public override bool SupportsBlockSize
        {
            get { return false; }
        }
    }

   
}