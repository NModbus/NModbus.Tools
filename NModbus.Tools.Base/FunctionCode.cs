namespace NModbus.Tools.Base
{
    /// <summary>
    /// Modbus function code constants
    /// http://en.wikipedia.org/wiki/Modbus
    /// </summary>
    public enum FunctionCode : byte
    {
        ReadDiscreteInputs = 2,

        ReadCoils = 1,

        ReadInputRegisters = 4,

        WriteSingleCoil = 5,

        WriteMultipleCoils = 15,

        ReadHoldingRegisters = 3,

        WriteSingleRegister = 6,

        MaskWriteRegsiter = 22,

        ReadFIFOQueue = 24,

        ReadFileRecord = 20,

        WriteFileRecord = 21,

        ReadExceptionStatus = 7,

        Diagnostic = 8,

        GetComEventCounter = 11,

        GetComEventLog = 12,

        ReportSlaveId = 17,

        ReadDeviceIdentification = 43,

        WriteMultipleRegisters = 16,
    }
}
