using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

using SixdofPathDataSendApp.Utils;
using SixdofPathDataSendApp.Models;

namespace SixdofPathDataSendApp
{
    public partial class FormMain : Form
    {
        bool isRecieve = false;
        string pathAndName = "";
        string fileNameRecieve = "";
        int readCount = 0;
        StreamReader stream;
        Thread recieveThread;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            JsonFileConfig.Instance.WriteToFile();
            if (stream != null)
            {
                stream.Dispose();
                stream = null;
            }
            if (recieveThread != null)
            {
                recieveThread.Abort();
                recieveThread = null;
            }
        }

        private void btnOpenAndSend_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                pathAndName = dialog.FileName;
                stream = File.OpenText(pathAndName);
                timerSend.Interval = Convert.ToInt32(numericInterval.Value);
                timerSend.Start();             
            }
        }

        private void timerSend_Tick(object sender, EventArgs e)
        {
            if (stream == null)
                return;
            if (stream.EndOfStream == true)
            {
                timerSend.Stop();
                MessageBox.Show("发送完毕！");
                return;
            }            
            var str = stream.ReadLine();
            var strs = str.Split(' ', ',', '|');
            DataPackageSender.Instance.SendDatas(strs);
            readCount++;
        }

        private void btnStartRecieve_Click(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            isRecieve = !isRecieve;
            btn.Text = isRecieve == true ? "停止接收" : "开始接收";
            if (Directory.Exists("./datas/") == false)
            {
                Directory.CreateDirectory("./datas/");
            }
            fileNameRecieve = "./datas/datapath" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt";
            if (recieveThread == null)
            {
                recieveThread = new Thread(new ThreadStart(() => {
                    while (true)
                    {
                        File.AppendAllText(fileNameRecieve, "0 0 0 0\r\n");
                        DataPackageSender.Instance.RecieveAndRecord(fileNameRecieve, isRecieve);
                        Thread.Sleep(40);
                    }
                }));
                recieveThread.Start();
            }
        }
    }
}
