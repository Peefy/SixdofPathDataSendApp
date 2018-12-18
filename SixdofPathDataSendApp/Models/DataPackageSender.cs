using System;
using System.IO;
using System.IO.Ports;

using SixdofPathDataSendApp.Utils;

namespace SixdofPathDataSendApp.Models
{
    public class DataPackageSender
    {

        const int BufferMax = 4096;
        const int PackageLength = 24;

        byte[] buffer = new byte[BufferMax];

        int usRxLength = 0;

        static DataPackageSender _sender;

        public static DataPackageSender Instance => _sender ?? (_sender = new DataPackageSender());

        SerialPort serialPort;
        SerialPortConfg config;

        object locked = new object();

        public DataPackageSender()
        {
            config = JsonFileConfig.Instance.SerialPortConfg;
            serialPort = new SerialPort
            {
                PortName = config.PortName,
                BaudRate = config.BaudRate
            };
            serialPort.Open();
        }

        public void SendDatas(double[] vels, double[] accs, double[] positions)
        {
            lock (locked)
            {
                if (serialPort.IsOpen == false)
                    return;
                if (positions == null || positions.Length <= 6)
                    return;
                var data = new LandVisionDataPackage()
                {
                    X = (short)(positions[0] / config.XYZScale),
                    Y = (short)(positions[1] / config.XYZScale),
                    Z = (short)(positions[2] / config.XYZScale),
                    Roll = (short)(positions[3] / config.XYZScale),
                    Pitch = (short)(positions[5] / config.XYZScale)
                };
                data.Crc = LandVisionDataPackage.CalCrc(data);
                var bytes = data.GetBytes(config.SendHeader, config.SendTail);
                serialPort.Write(bytes, 0, bytes.Length);
            }
        }

        public void SendDatas(string[] datas)
        {
            lock(locked)
            {
                if (serialPort.IsOpen == false)
                    return;
                if (datas == null || datas.Length < 18)
                    return;
                if (double.TryParse(datas[0 + 12], out var x) == true &&
                    double.TryParse(datas[1 + 12], out var y) == true &&
                    double.TryParse(datas[2 + 12], out var z) == true &&
                    double.TryParse(datas[3 + 12], out var roll) == true &&
                    double.TryParse(datas[4 + 12], out var yaw) == true &&
                    double.TryParse(datas[5 + 12], out var pitch) == true)
                {
                    var data = new LandVisionDataPackage()
                    {
                        X = (short)(x / config.XYZScale),
                        Y = (short)(y / config.XYZScale),
                        Z = (short)(z / config.XYZScale),
                        Roll = (short)(roll / config.XYZScale),
                        Pitch = (short)(pitch / config.XYZScale)
                    };
                    data.Crc = LandVisionDataPackage.CalCrc(data);
                    var bytes = data.GetBytes(config.SendHeader, config.SendTail);
                    serialPort.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public void RecieveAndRecord(string pathAndName, bool isWriteFile)
        {
            lock (locked)
            {
                if (serialPort.IsOpen == false)
                    return;
                if (string.IsNullOrEmpty(pathAndName) == true)
                    return;
                var nowlength = serialPort.BytesToRead;
                serialPort.Read(buffer, 0, nowlength);
                usRxLength += nowlength;
                while (usRxLength >= PackageLength)
                {
                    if (buffer[0] != config.SendHeader)
                    {
                        usRxLength--;
                        Array.Copy(buffer, 0, buffer, 1, usRxLength);
                        continue;
                    }
                    var data = new LandVisionDataPackage();
                    data.SetBytes(buffer);
                    if (data.Header == config.SendHeader && data.Tail == config.SendTail &&
                        LandVisionDataPackage.CalCrc(data) == data.Crc && 
                        isWriteFile == true)
                    {
                        File.AppendAllText(pathAndName, $"{0} {0} {0} {data.XAcc * config.AccScale} {data.YAcc * config.AccScale} {0} {0} {0} {0} {0} {0} {0} {data.X * config.XYZScale} {data.Y * config.XYZScale} {data.Z * config.XYZScale} {data.Roll * config.AngleScale} {0} {data.Pitch * config.AngleScale}\r\n");
                    }
                    usRxLength -= PackageLength;
                    Array.Copy(buffer, 0, buffer, PackageLength, usRxLength);
                }
            }
        }
    }
}

