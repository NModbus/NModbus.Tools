namespace NModbus.Tools.FrameParser.Model
{
    public class ReadInputRegistersFunctionService : ReadRegistersFunctionService
    {
        public ReadInputRegistersFunctionService() 
            : base(Base.FunctionCode.ReadInputRegisters)
        {
        }
    }
}