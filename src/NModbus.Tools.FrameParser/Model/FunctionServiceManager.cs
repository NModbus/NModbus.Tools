using System;
using System.Collections.Generic;
using System.Linq;
using NModbus.Tools.Base;

namespace NModbus.Tools.FrameParser.Model
{
    public static class FunctionServiceManager
    {
        private static readonly Dictionary<FunctionCode, IFunctionService> _functionServices;

        static FunctionServiceManager()
        {
            var services = new IFunctionService[]
            {
                new ReadHoldingRegistersFunctionService(),
                new ReadInputRegistersFunctionService(), 
                new WriteRegistersFunctionService(), 
                new WriteSingleRegisterFunctionService(), 
            };

            _functionServices = services.ToDictionary(s => s.FunctionCode, s => s);
        }

        public static FunctionServiceResult Process(byte[] frame)
        {
            try
            {
                if (frame.Length < 5)
                {
                    return new FunctionServiceResult(null, "Frame not long enough.");
                }

                if (!frame.DoesCrcMatch())
                {
                    return new FunctionServiceResult(error: "Invalid CRC");
                }

                byte rawFunctionCode = frame[1];

                FunctionCode functionCode = (FunctionCode)(rawFunctionCode & 0x7F);

                if ((rawFunctionCode & 0x80) > 0)
                {
                    var exceptionDescription = SlaveExceptionDescriptionFactory.GetExceptionDescription(frame[2]);

                    return new FunctionServiceResult(error: exceptionDescription);
                }

                if (_functionServices.TryGetValue(functionCode, out var service))
                {
                    return service.Process(frame);
                }

                return new FunctionServiceResult(error: $"Unsupported function code: {rawFunctionCode}");

            }
            catch (Exception ex)
            {
                return new FunctionServiceResult(error: ex.Message);
            }
        }
    }
}