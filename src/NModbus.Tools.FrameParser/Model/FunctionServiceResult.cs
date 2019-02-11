using NModbus.Tools.Base;
using NModbus.Tools.FrameParser.ViewModel;

namespace NModbus.Tools.FrameParser.Model
{
    public class FunctionServiceResult
    {
        public FunctionServiceResult(
            byte? unitId = null, 
            string summary = null, 
            FrameElement[] elements = null, 
            PacketType? packetType = null, 
            string error = null,
            FunctionCode? functionCode = null)
        {
            UnitId = unitId;
            Elements = elements;
            Summary = summary;
            PacketType = packetType;
            Error = error;
            FunctionCode = functionCode;
        }

        public string Summary { get; }

        public byte? UnitId { get; }

        public FrameElement[] Elements { get; }

        public PacketType? PacketType { get; }

        public string Error { get; }
        public FunctionCode? FunctionCode { get; }
    }
}