namespace SixdofPathDataSendApp
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerSend = new System.Windows.Forms.Timer(this.components);
            this.btnOpenAndSend = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.numericInterval = new System.Windows.Forms.NumericUpDown();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // timerSend
            // 
            this.timerSend.Interval = 50;
            this.timerSend.Tick += new System.EventHandler(this.timerSend_Tick);
            // 
            // btnOpenAndSend
            // 
            this.btnOpenAndSend.Location = new System.Drawing.Point(23, 78);
            this.btnOpenAndSend.Name = "btnOpenAndSend";
            this.btnOpenAndSend.Size = new System.Drawing.Size(139, 50);
            this.btnOpenAndSend.TabIndex = 0;
            this.btnOpenAndSend.Text = "载入文件\r\n并发送";
            this.btnOpenAndSend.UseVisualStyleBackColor = true;
            this.btnOpenAndSend.Click += new System.EventHandler(this.btnOpenAndSend_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "发送间隔";
            // 
            // numericInterval
            // 
            this.numericInterval.Location = new System.Drawing.Point(80, 38);
            this.numericInterval.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericInterval.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericInterval.Name = "numericInterval";
            this.numericInterval.Size = new System.Drawing.Size(82, 21);
            this.numericInterval.TabIndex = 2;
            this.numericInterval.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(187, 162);
            this.Controls.Add(this.numericInterval);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOpenAndSend);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "路谱发送软件";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericInterval)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timerSend;
        private System.Windows.Forms.Button btnOpenAndSend;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericInterval;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

