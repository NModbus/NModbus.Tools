using System;
using System.Linq;
using CaptiveAire.EndianUtil.Conversion;
using NModbus.Tools.Base;
using NModbus.Tools.FrameParser.ViewModel;

namespace NModbus.Tools.FrameParser.Model
{
    public abstract class RegistersFunctionService : FunctionServiceBase
    {
        protected const int NumberOfRegistersMessageLength = 8;

        protected RegistersFunctionService(FunctionCode functionCode) 
            : base(functionCode)
        {
        }

        protected FunctionServiceResult ProcessNumberOfRegisters(byte[] frame, PacketType? packetType)
        {
            if (frame.Length != NumberOfRegistersMessageLength)
                throw new ArgumentException($"The message must be 8 bytes long. It was {frame.Length}.");

            
            ushort address = EndianBitConverter.Big.ToUInt16(frame, 2);
            ushort number = EndianBitConverter.Big.ToUInt16(frame, 4);

            var summary = $"Read {number} registers starting at {address}";

            return new FunctionServiceResult(frame[0], summary, packetType: packetType, functionCode: FunctionCode);
        }

        protected FunctionServiceResult ProcessReadRegisters(byte[] frame, PacketType? packetType)
        {

            //We'll assume that this is a response
            byte numberOfBytes = frame[2];

            //Get the number of registers
            var numberOfRegisters = numberOfBytes / 2;

            //Get the registers themselves
            var registers = frame.GetRegisters(numberOfRegisters, 3);

            var elements = registers
                .Select((r, i) => new FrameElement($"{i}", $"{r}"))
                .ToArray();

            var summary = $"{numberOfRegisters} registers.";

            return new FunctionServiceResult(frame[0], summary, elements, packetType, functionCode: FunctionCode);
        }
    }
}