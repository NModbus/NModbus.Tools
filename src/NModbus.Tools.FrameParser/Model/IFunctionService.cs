using NModbus.Tools.Base;

namespace NModbus.Tools.FrameParser.Model
{
    public interface IFunctionService
    {
        FunctionCode FunctionCode { get; }

        FunctionServiceResult Process(byte[] frame);
    }
}