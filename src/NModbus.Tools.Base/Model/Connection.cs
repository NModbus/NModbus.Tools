using System.IO.Ports;

namespace NModbus.Tools.Base.Model
{
    /// <summary>
    /// Represents a Modbus connection
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// Gets or sets the name of the connection.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of connection.
        /// </summary>
        public ConnectionType Type { get; set; }

        /// <summary>
        /// The hostname for UDP/TCP connections.
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// The port for UDP/TCP connections.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The serial port name for RTU/ASCII connections.
        /// </summary>
        public string SerialPortName { get; set; }

        public int Baud { get; set; }

        public StopBits StopBits { get; set; }

        public byte DataBits { get; set; }

        public Parity Parity { get; set; }

        /// <summary>
        /// The read timeout in milliseconds.
        /// </summary>
        public int ReadTimeout { get; set; }

        /// <summary>
        /// The write timeout in milliseconds.
        /// </summary>
        public int WriteTimeout { get; set; }
    }
}
