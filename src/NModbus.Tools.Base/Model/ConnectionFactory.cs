using NModbus.Serial;
using System;
using System.IO.Ports;
using System.Net.Sockets;

namespace NModbus.Tools.Base.Model
{
    public class ConnectionFactory : IModbusMasterFactory
    {
        private const int DefaultTcpConnectionTimeoutMilliseconds = 5 * 1000;
        private readonly Connection _connection;

        public ConnectionFactory(Connection connection)
        {
            _connection = connection;
        }

        public IModbusMaster Create()
        {
            var factory = new ModbusFactory();

            switch (_connection.Type)
            {
                case ConnectionType.Rtu:
                    {
                        var serialPort = CreateAndOpenSerialPort(_connection);

                        var transport = new SerialPortAdapter(serialPort);

                        return factory.CreateRtuMaster(transport);
                    }
                case ConnectionType.Ascii:
                    {
                        var serialPort = CreateAndOpenSerialPort(_connection);

                        var transport = new SerialPortAdapter(serialPort);

                        return factory.CreateRtuMaster(transport);
                    }
                case ConnectionType.Tcp:
                    {
                        var tcpClient = new TcpClient
                        {
                            ReceiveTimeout = _connection.ReadTimeout,
                            SendTimeout = _connection.WriteTimeout
                        };

                        var effectiveConnectionTimeout = _connection.ConnectionTimeout;

                        if (effectiveConnectionTimeout <= 0)
                        {
                            effectiveConnectionTimeout = DefaultTcpConnectionTimeoutMilliseconds;
                        }

                        if (!tcpClient.ConnectAsync(_connection.HostName, _connection.Port).Wait(effectiveConnectionTimeout))
                        {
                            tcpClient.Dispose();

                            throw new TimeoutException($"Timed out trying to connect to TCP Modbus device at {_connection.HostName}:{_connection.Port}");
                        }

                        return factory.CreateMaster(tcpClient);
                    }
                case ConnectionType.Udp:
                    {
                        var udpClient = new UdpClient();
                        
                        return factory.CreateMaster(udpClient);
                    }
                default:
                    throw new ArgumentException($"{nameof(_connection.Type)} had an unepected value '{_connection.Type}'.");
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

        public override string ToString()
        {
            switch (_connection.Type)
            {
                case ConnectionType.Tcp:
                    return $"{_connection.Name} [TCP {_connection.HostName}:{_connection.Port}]";

                case ConnectionType.Udp:
                    return $"{_connection.Name} [UDP {_connection.HostName}:{_connection.Port}]";

                case ConnectionType.Rtu:
                    return $"{_connection.Name} [RTU {_connection.SerialPortName} ({_connection.Baud})]";

                case ConnectionType.Ascii:
                    return $"{_connection.Name} [ASCII {_connection.SerialPortName} ({_connection.Baud})]";

                default:
                    return "Unknown Type";
            }
        }
    }
}
