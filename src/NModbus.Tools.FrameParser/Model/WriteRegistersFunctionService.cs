using System.Linq;
using CaptiveAire.EndianUtil.Conversion;
using NModbus.Tools.FrameParser.ViewModel;

namespace NModbus.Tools.FrameParser.Model
{
    public class WriteRegistersFunctionService : RegistersFunctionService
    {
        public WriteRegistersFunctionService() 
            : base(Base.FunctionCode.WriteMultipleRegisters)
        {
        }

        private FunctionServiceResult ProcessWriteRegisters(byte[] frame)
        {
            // http://www.simplymodbus.ca/FC16.htm

            var dataAddress = EndianBitConverter.Big.ToUInt16(frame, 2);
            var numberOfRegisters = EndianBitConverter.Big.ToUInt16(frame, 4);

            var bytesToFollow = frame[6];

            if (bytesToFollow != numberOfRegisters*2)
            {
                return
                    new FunctionServiceResult(frame[0],
                        $"The bytes to follow {bytesToFollow} does not match the number of registers {numberOfRegisters}.");
            }

            var registers = frame.GetRegisters(numberOfRegisters, 7);

            var elements = registers
                .Select((r, i) => new FrameElement($"{i}", $"{r}"))
                .ToArray();

            var summary = $"Write {numberOfRegisters} registers starting at register {dataAddress}";

            return new FunctionServiceResult(frame[0], summary, elements, PacketType.Request, functionCode: FunctionCode);
        }

        public override FunctionServiceResult Process(byte[] frame)
        {
            if (frame.Length == NumberOfRegistersMessageLength)
            {
                return ProcessNumberOfRegisters(frame, PacketType.Response);
            }

            return ProcessWriteRegisters(frame);
        }
    }
}