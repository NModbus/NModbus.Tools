namespace NModbus.Tools.FrameParser.Model
{
    public static class SlaveExceptionDescriptionFactory
    {
        public static string GetExceptionDescription(byte exceptionCode)
        {
            switch (exceptionCode)
            {
                case 1:
                    return "Illegal Function";

                case 2:
                    return "Illegal Data Address";

                case 3:
                    return "Illegal Data Value";

                case 4:
                    return "Slave Device Failure";

                case 5:
                    return "Acknowledge";

                case 6:
                    return "Slave Device Busy";

                case 7:
                    return "Negative Acknowledgement";

                case 8:
                    return "Memory Parity Error";

                case 10:
                    return "Gateway Path Unavailable";

                case 11:
                    return "Gateway Target Device Failed to Respond";

                default:
                    return $"Unexpected error: {exceptionCode}";
            }
        }
    }
}