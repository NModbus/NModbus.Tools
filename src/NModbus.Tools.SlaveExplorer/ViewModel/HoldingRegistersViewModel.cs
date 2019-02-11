namespace NModbus.Tools.SlaveExplorer.ViewModel
{
    public class HoldingRegistersViewModel : PointsViewModelBase<RegisterViewModel, ushort>
    {
        public HoldingRegistersViewModel(ISlaveExplorerContext context) : base(context)
        {
        }

        protected override ushort[] ReadCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, ushort numberOfPoints)
        {
            return modbusMaster.ReadHoldingRegisters(slaveId, startAddress, numberOfPoints);
        }

        protected override void WriteCore(IModbusMaster modbusMaster, byte slaveId, ushort startAddress, ushort[] values)
        {
            modbusMaster.WriteMultipleRegisters(slaveId, startAddress, values);
        }

        public override bool IsWriteable
        {
            get { return true; }
        }

        public override bool SupportsBlockSize
        {
            get { return true; }
        }
    }
}