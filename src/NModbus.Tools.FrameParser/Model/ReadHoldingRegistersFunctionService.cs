namespace NModbus.Tools.FrameParser.Model
{
    public class ReadHoldingRegistersFunctionService : ReadRegistersFunctionService
    {
        public ReadHoldingRegistersFunctionService() 
            : base(Base.FunctionCode.ReadHoldingRegisters)
        {
        }

     
    }
}