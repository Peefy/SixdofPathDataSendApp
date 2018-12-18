using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace SixdofPathDataSendApp.Models
{
    public class LandVisionDataPackage
    {
        public byte Header;
        public short X;
        public short Y;
        public short Z;
        public short XAcc;
        public short YAcc;
        public byte RoadTypeBefore;
        public byte RoadType;
        public short Pitch;
        public short Roll;
        public byte ControlByte;
        public byte NoneByte;
        public byte CmdPcDataAcksByte;
        public byte FunctionsByte;
        public ushort Crc;
        public byte Tail;

        public static ushort CalCrc(byte[] bytes, int startIndex, int length)
        {
            ushort crc = 0;
            for (int i = startIndex; i < startIndex + length; ++i)
            {
                crc += bytes[i];
            }
            return crc;
        }

        public static ushort CalCrc(LandVisionDataPackage data)
        {
            ushort crc = 0;
            crc += (byte)data.X;
            crc += (byte)(data.X >> 8);
            crc += (byte)data.Y;
            crc += (byte)(data.Y >> 8);
            crc += (byte)data.Z;
            crc += (byte)(data.Z >> 8);
            crc += (byte)data.XAcc;
            crc += (byte)(data.XAcc >> 8);
            crc += (byte)data.YAcc;
            crc += (byte)(data.YAcc >> 8);
            crc += (byte)data.RoadTypeBefore;
            crc += (byte)data.RoadType;
            crc += (byte)data.Pitch;
            crc += (byte)(data.Pitch >> 8);
            crc += (byte)data.Roll;
            crc += (byte)(data.Roll >> 8);
            crc += (byte)data.ControlByte;
            crc += (byte)data.NoneByte;
            crc += (byte)data.CmdPcDataAcksByte;
            crc += (byte)data.FunctionsByte;
            return crc;
        }

        public byte[] GetBytes(byte header, byte tail)
        {
            return new byte[]
            {
                Header = header,
                (byte)X,
                (byte)(X >> 8),
                (byte)Y,
                (byte)(Y >> 8),
                (byte)Z,
                (byte)(Z >> 8),
                (byte)XAcc,
                (byte)(XAcc >> 8),
                (byte)YAcc,
                (byte)(YAcc >> 8),
                (byte)RoadTypeBefore,
                (byte)RoadType,
                (byte)Pitch,
                (byte)(Pitch >> 8),
                (byte)Roll,
                (byte)(Roll >> 8),
                (byte)ControlByte,
                (byte)NoneByte,
                (byte)CmdPcDataAcksByte,
                (byte)FunctionsByte,
                (byte)Crc,
                (byte)(Crc >> 8),
                Tail = tail
            };
        }

        public void SetBytes(byte[] bytes)
        {
            Header = bytes[0];
            X = (short)(bytes[1] + bytes[2] << 8);
            Y = (short)(bytes[3] + bytes[4] << 8);
            Z = (short)(bytes[5] + bytes[6] << 8);
            XAcc = (short)(bytes[7] + bytes[8] << 8);
            YAcc = (short)(bytes[9] + bytes[10] << 8);
            RoadTypeBefore = bytes[11];
            RoadType = bytes[12];
            Pitch = (short)(bytes[13] + bytes[14] << 8);
            Roll = (short)(bytes[15] + bytes[16] << 8);
            ControlByte = bytes[17];
            NoneByte = bytes[18];
            CmdPcDataAcksByte = bytes[19];
            FunctionsByte = bytes[20];
            Crc = (ushort)(bytes[21] + bytes[22] << 8);
            Tail = bytes[23];
        }
    }
}

