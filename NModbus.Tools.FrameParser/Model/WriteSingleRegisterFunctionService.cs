using NModbus.Tools.Base;
using NModbus.Tools.FrameParser.ViewModel;

namespace NModbus.Tools.FrameParser.Model
{
    public class WriteSingleRegisterFunctionService : FunctionServiceBase
    {
        private const int FrameLength = 8;

        public WriteSingleRegisterFunctionService() 
            : base(FunctionCode.WriteSingleRegister)
        {
        }

        public override FunctionServiceResult Process(byte[] frame)
        {
            if (frame.Length != FrameLength)
            {
                return new FunctionServiceResult(error: $"Frame length was {frame.Length}. Expected {FrameLength}.");
            }

            ushort address = frame.GetRegister(2);

            ushort value = frame.GetRegister(4);

            var elements = new FrameElement[]
            {
                new FrameElement("Address", $"{address}"),
                new FrameElement("Value", $"{value}"),
            };

            return new FunctionServiceResult(frame[0], "Read / Write Single Register", elements);
        }
    }
}