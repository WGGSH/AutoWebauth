using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;

namespace AutoWebauth
{
    public partial class Form1 : Form
    {
        private RunspaceInvoke invoker;
        private string SSID = ""; // 接続中のSSID

        public Form1()
        {
            InitializeComponent();
            this.invoker = new RunspaceInvoke();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 通知領域のアイコンを設定
            this.notifyIcon1.Icon= SystemIcons.Application;

            // 2つのタイマーを起動する
            this.timerAuth.Start();
            this.timerSSID.Start();

            this.SSID = this.GetSSID();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timerSSID_Tick(object sender, EventArgs e)
        {
            this.SSID = this.GetSSID();
        }

        private void timerAuth_Tick(object sender, EventArgs e)
        {
            if (this.SSID == "Rits-Webauth") MessageBox.Show("auth");
            
        }

        private string GetSSID()
        {
            string ssid = "";
            string command = "$(netsh wlan show i)[19].split(':')[1].trim()";
            var str=this.invoker.Invoke(command);
            foreach (var r in str)
            {
                ssid += r;
            }
            return ssid;
        }
    }
}
