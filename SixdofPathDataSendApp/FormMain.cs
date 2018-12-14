using System;
using System.IO;
using System.Windows.Forms;

using SixdofPathDataSendApp.Utils;
using SixdofPathDataSendApp.Models;

namespace SixdofPathDataSendApp
{
    public partial class FormMain : Form
    {
        string pathAndName = "";
        int readCount = 0;
        StreamReader stream;

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
    }
}
