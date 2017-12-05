using System.Collections.Generic;
using CaptiveAire.EndianUtil.Conversion;
using NModbus.Tools.FrameParser.ViewModel;

namespace NModbus.Tools.FrameParser
{
    public static class FrameExtensions
    {
        public static void Add(this IList<FrameElement> list, string name, string value = null,
            string description = null)
        {
            list.Add(new FrameElement(name, value, description));
        }

        public static ushort[] GetRegisters(this byte[] frame, int numberOfRegisters, int startOffset)
        {
            int currentOffset = startOffset;

            var registers = new ushort[numberOfRegisters];

            for (int registerIndex = 0; registerIndex < numberOfRegisters; registerIndex++)
            {
                //Convert each register
                registers[registerIndex] = frame.GetRegister(currentOffset);

                //Move to the next offset
                currentOffset += 2;
            }

            return registers;
        }

        public static ushort GetRegister(this byte[] frame, int offset)
        {
            return EndianBitConverter.Big.ToUInt16(frame, offset);
        }
    }
}