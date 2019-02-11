using NModbus.Tools.Base;

namespace NModbus.Tools.FrameParser.Model
{
    public abstract class FunctionServiceBase : IFunctionService
    {
        private readonly FunctionCode _functionCode;

        protected FunctionServiceBase(FunctionCode functionCode)
        {
            _functionCode = functionCode;
        }

        public FunctionCode FunctionCode
        {
            get { return _functionCode; }
        }

        public abstract FunctionServiceResult Process(byte[] frame);
    }

    
}