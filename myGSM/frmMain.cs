using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Ports;
//本程序没有作完整性校验，请正确使用
namespace myGSM
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        myGSMModem gsm;
        delegate void UpdataDelegate();
        UpdataDelegate UpdateHandle = null;

        void my_OnRecieved(object sender, EventArgs e)
        {
            if (this.IsHandleCreated) { Invoke(UpdateHandle, null); }
        }

        void Updata1()
        {
            txtReceiveMsg.Text = "收到新消息:" + gsm.ReadNewMsg();
        }

        private void changeStatus(bool b)
        {
            foreach (Control c in gbox1.Controls)
            {
                if (c is Button) { c.Enabled = b; }
            }
            gbox2.Enabled = b;
            gbox3.Enabled = b;
            gbox4.Enabled = b;
            btnOpen.Enabled = true;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            cboxPort.Items.AddRange(SerialPort.GetPortNames());
            if (cboxPort.Items.Count > 0)
            {
                cboxPort.SelectedIndex = 0;
            }
            this.changeStatus(false);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cboxPort.Items.Count == 0)
            {
                return;
            }
            if (btnOpen.Text == "打开设备")
            {
                gsm = new myGSMModem(cboxPort.SelectedItem.ToString(), 9600);
                gsm.GetNewMsg += new myGSMModem.OnRecievedHandler(my_OnRecieved);
                UpdateHandle = new UpdataDelegate(Updata1);
                gsm.OpenComm();
                btnOpen.Text = "关闭设备";
                labStatus.ForeColor = Color.Green;
                labStatus.Text = "已连接";
                this.changeStatus(true);
            }
            else
            {
                gsm.CloseComm();
                btnOpen.Text = "打开设备";
                labStatus.ForeColor = Color.Red;
                labStatus.Text = "未连接";
                this.changeStatus(false);
            }
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("拨打电话会占用很长时间，界面会处于卡死状态，确定要拨打电话吗？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gsm.Call(txtTel.Text.Trim());
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            if (rbtnText.Checked)
            {
                richTextBox.AppendText($"发送短信:{gsm.SendMsgText(txtTel.Text.Trim(), txtContent.Text.Trim())}\r\n");
            }
            else
            {
                richTextBox.AppendText($"发送短信:{ gsm.SendMsg(txtTel.Text.Trim(), txtContent.Text.Trim())}\r\n");
            }
        }

        private void btnGetJQM_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText($"机器码:{gsm.GetMachineNo()}\r\n");
        }

        private void btnGetNum_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText($"服务中心号码:{ gsm.GetMsgCenterNo()}\r\n");
        }

        private void btnGetAllMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetAllMsg();
            richTextBox.AppendText($"获取所有短信成功，总共{ msg.Count}条\r\n");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    richTextBox.AppendText($"{s}\r\n");
                }
            }
        }

        private void btnGetUnReadMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetUnReadMsg();
            richTextBox.AppendText($"获取未读短信成功，总共{ msg.Count}条\r\n");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    richTextBox.AppendText($"{s}\r\n");
                }
            }
        }

        private void btnGetUnSendMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetUnSendMsg();
            richTextBox.AppendText($"获取未发短信成功，总共{ msg.Count}条\r\n");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    richTextBox.AppendText($"{s}\r\n");
                }
            }
        }

        private void btnGetIsReadMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetIsReadMsg();
            richTextBox.AppendText($"获取已读短信成功，总共{msg.Count}条\r\n");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    richTextBox.AppendText($"{s}\r\n");
                }
            }
        }

        private void btnGetIsSendMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetIsSendMsg();
            richTextBox.AppendText($"获取已发短信成功，总共{msg.Count}条\r\n");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    richTextBox.AppendText($"{s}\r\n");
                }
            }
        }

        private void btnGetMsgByIndex_Click(object sender, EventArgs e)
        {
            string msg = gsm.ReadMsgByIndex(Convert.ToInt32(txtxuhao1.Text.Trim()));
            richTextBox.AppendText("获取指定序号短信成功\r\n");
            if (msg.Length != 0)
            {
                richTextBox.AppendText($"{msg}\r\n");
            }
        }

        private void btnDeleteMsgByIndex_Click(object sender, EventArgs e)
        {
            if (gsm.DelMsgByIndex(Convert.ToInt32(txtxuhao2.Text.Trim())))
            {
                richTextBox.AppendText("删除指定序号短信成功\r\n");
            }
            else
            {
                richTextBox.AppendText("删除指定序号短信失败\r\n");
            }
        }

        private void btnSendAT_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText("发送AT命令成功\r\n");
            richTextBox.AppendText($"返回:{ gsm.SendAT(txtAT.Text.Trim())}\r\n");
        }

        private void btnCheckConn_Click(object sender, EventArgs e)
        {
            if (gsm.isConnect())
            {
                richTextBox.AppendText("连接成功\r\n");
            }
            else
            {
                richTextBox.AppendText("连接失败\r\n");
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (gsm.DelAllMsg())
            {
                richTextBox.AppendText("删除所有短信成功\r\n");
            }
            else
            {
                richTextBox.AppendText("删除所有短信失败\r\n");
            }
        }

        private void btnIsAutoDel_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText($"是否自动删除:{gsm.AutoDelMsg}\r\n");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().MaxWorkingSet = new IntPtr(750000);
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            richTextBox.Text = "";
        }

        private void getIMEI_Click(object sender, EventArgs e)
        {
            richTextBox.AppendText($"IMEI:{ gsm.GeIMEINo()}\r\n");
        }
    }
}