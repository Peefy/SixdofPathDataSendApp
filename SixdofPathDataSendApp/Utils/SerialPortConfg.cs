using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;

namespace SixdofPathDataSendApp.Utils
{
    public class SerialPortConfg
    {
        [JsonProperty("portName")]
        public string PortName { get; set; } = "COM1";

        [JsonProperty("baudRate")]
        public int BaudRate { get; set; } = 57600;

        [JsonProperty("sendHeader")]
        public byte SendHeader { get; set; } = (byte)'P';

        [JsonProperty("sendTail")]
        public byte SendTail { get; set; } = (byte)'#';

        [JsonProperty("xyzScale")]
        public double XYZScale { get; set; } = 0.1;

        [JsonProperty("accScale")]
        public double AccScale { get; set; } = 0.1;

        [JsonProperty("angleScale")]
        public double AngleScale { get; set; } = 0.01;
    }
}
