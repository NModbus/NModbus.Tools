using NModbus.Serial;
using System;
using System.IO.Ports;
using System.Net.Sockets;

namespace NModbus.Tools.Base.Model
{
    public class ConnectionFactory
    {
        private const int TcpConnectionTimeoutMilliseconds = 10 * 1000;

        public IModbusMaster CreateAsync(Connection connection)
        {
            var factory = new ModbusFactory();

            switch (connection.Type)
            {
                case ConnectionType.Rtu:
                    {
                        var serialPort = CreateAndOpenSerialPort(connection);

                        var transport = new SerialPortAdapter(serialPort);

                        return factory.CreateRtuMaster(transport);
                    }
                case ConnectionType.Ascii:
                    {
                        var serialPort = CreateAndOpenSerialPort(connection);

                        var transport = new SerialPortAdapter(serialPort);

                        return factory.CreateRtuMaster(transport);
                    }
                case ConnectionType.Tcp:
                    {
                        var tcpClient = new TcpClient();

                        if (!tcpClient.ConnectAsync(connection.HostName, connection.Port).Wait(TcpConnectionTimeoutMilliseconds))
                        {
                            throw new TimeoutException($"Timed out trying to connect to TCP Modbus device at {connection.HostName}:{connection.Port}");
                        }

                        return factory.CreateMaster(tcpClient);
                    }
                case ConnectionType.Udp:
                    {
                        var udpClient = new UdpClient();
                        
                        return factory.CreateMaster(udpClient);
                    }
                default:
                    throw new ArgumentException($"{nameof(connection.Type)} had an unepected value '{connection.Type}'.");
            }
        }

        private SerialPort CreateAndOpenSerialPort(Connection connection)
        {
            if (string.IsNullOrWhiteSpace(connection.SerialPortName))
                throw new ArgumentException($"{nameof(connection.SerialPortName)} was not provided.");

            if (connection.Baud <= 0)
                throw new ArgumentException($"{nameof(connection.Baud)} had an invalid value.");

            var serialPort = new SerialPort(connection.SerialPortName, connection.Baud, connection.Parity, connection.DataBits, connection.StopBits);

            serialPort.ReadTimeout = connection.ReadTimeout;
            serialPort.WriteTimeout = connection.WriteTimeout;

            serialPort.Open();

            return serialPort;
        }
    }
}
