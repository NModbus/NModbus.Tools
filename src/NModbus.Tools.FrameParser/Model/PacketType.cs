namespace NModbus.Tools.FrameParser.Model
{
    public enum PacketType
    {
        /// <summary>
        /// This is a request from the master.
        /// </summary>
        Request,

        /// <summary>
        /// This is a response from a slave.
        /// </summary>
        Response,

        /// <summary>
        /// Unsure.
        /// </summary>
        Unknown
    }
}