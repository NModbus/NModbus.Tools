using NModbus.Tools.Base;

namespace NModbus.Tools.FrameParser.Model
{
    public abstract class ReadRegistersFunctionService : RegistersFunctionService
    {
        protected ReadRegistersFunctionService(FunctionCode functionCode) 
            : base(functionCode)
        {
        }

        public override FunctionServiceResult Process(byte[] frame)
        {
            if (frame.Length == NumberOfRegistersMessageLength)
            {
                return ProcessNumberOfRegisters(frame, PacketType.Request);
            }

            return ProcessReadRegisters(frame, PacketType.Response);
        }
    }
}