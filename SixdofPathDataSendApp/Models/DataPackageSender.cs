using System.IO.Ports;

using SixdofPathDataSendApp.Utils;

namespace SixdofPathDataSendApp.Models
{
    public class DataPackageSender
    {

        static DataPackageSender _sender;

        public static DataPackageSender Instance => _sender ?? (_sender = new DataPackageSender());

        SerialPort serialPort;
        SerialPortConfg config;

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

        public void SendDatas(string[] datas)
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
}

