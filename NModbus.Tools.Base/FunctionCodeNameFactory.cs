
namespace NModbus.Tools.Base
{
    public static class FunctionCodeDescriptionFactory
    {
        public static string GetFunctionCodeDescription(byte functionCode)
        {
            //Strip off the error bit
            FunctionCode functionCodeOnly = (FunctionCode)(functionCode & 0x7F);

            string description;

            switch (functionCodeOnly)
            {
                case FunctionCode.ReadDiscreteInputs:
                    description = "Read Discrete Inputs";
                    break;

                case FunctionCode.ReadCoils:
                    description = "ReadCoils";
                    break;

                case FunctionCode.ReadInputRegisters:
                    description = "Read Input Registers";
                    break;

                case FunctionCode.WriteSingleCoil:
                    description = "Write Single Coil";
                    break;

                case FunctionCode.WriteMultipleCoils:
                    description = "Write Multiple Coils";
                    break;

                case FunctionCode.ReadHoldingRegisters:
                    description = "Read Holding Registers";
                    break;

                case FunctionCode.WriteSingleRegister:
                    description = "Write Single Register";
                    break;

                case FunctionCode.MaskWriteRegsiter:
                    description = "Mask Write Regsiter";
                    break;

                case FunctionCode.ReadFIFOQueue:
                    description = "Read FIFO Queue";
                    break;

                case FunctionCode.ReadFileRecord:
                    description = "Read File Record";
                    break;

                case FunctionCode.WriteFileRecord:
                    description = "Write File Record";
                    break;

                case FunctionCode.ReadExceptionStatus:
                    description = "Read Exception Status";
                    break;

                case FunctionCode.Diagnostic:
                    description = "Diagnostic";
                    break;

                case FunctionCode.GetComEventCounter:
                    description = "Get Com Event Counter";
                    break;

                case FunctionCode.GetComEventLog:
                    description = "Get Com Event Log";
                    break;

                case FunctionCode.ReportSlaveId:
                    description = "Report Slave Id";
                    break;

                case FunctionCode.ReadDeviceIdentification:
                    description = "Read Device Identification";
                    break;

                case FunctionCode.WriteMultipleRegisters:
                    description = "Write Multiple Registers";
                    break;

                default:
                    description = "UNKNOWN";
                    break;
            }

            bool isError = (functionCode & 0x80) > 0;

            if (isError)
            {
                return $"({(byte)functionCodeOnly}) - {description} Error";
            }

            return $"({(byte)functionCodeOnly}) - {description}";
        }
    }
}