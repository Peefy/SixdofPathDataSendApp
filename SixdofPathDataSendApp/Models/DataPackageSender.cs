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
            var data = new LandVisionDataPackage()
            {
                X = (short)(double.Parse(datas[0 + 12]) / config.XYZScale),
                Y = (short)(double.Parse(datas[1 + 12]) / config.XYZScale),
                Z = (short)(double.Parse(datas[2 + 12]) / config.XYZScale),
                Roll = (short)(double.Parse(datas[3 + 12]) / config.XYZScale),
                Pitch = (short)(double.Parse(datas[5 + 12]) / config.XYZScale)
            };
            data.Crc = LandVisionDataPackage.CalCrc(data);
            var bytes = data.GetBytes(config.SendHeader, config.SendTail);
            serialPort.Write(bytes, 0, bytes.Length);
        }

    }
}

