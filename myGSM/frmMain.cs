using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
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
                gsm = new myGSMModem(cboxPort.SelectedItem.ToString(),9600);
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
                lbox.Items.Add("发送短信:" + gsm.SendMsgText(txtTel.Text.Trim(), txtContent.Text.Trim()).ToString());
            }
            else
            {
                lbox.Items.Add("发送短信:" + gsm.SendMsg(txtTel.Text.Trim(), txtContent.Text.Trim()).ToString());
            }            
        }

        private void btnGetJQM_Click(object sender, EventArgs e)
        {
            lbox.Items.Add("机器码:" + gsm.GetMachineNo());
        }

        private void btnGetNum_Click(object sender, EventArgs e)
        {
            lbox.Items.Add("服务中心号码:" + gsm.GetMsgCenterNo());
        }

        private void btnGetAllMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetAllMsg();
            lbox.Items.Add("获取所有短信成功，总共" + msg.Count + "条");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    lbox.Items.Add(s);
                }
            }
        }

        private void btnGetUnReadMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetUnReadMsg();
            lbox.Items.Add("获取未读短信成功，总共" + msg.Count + "条");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    lbox.Items.Add(s);
                }
            }
        }

        private void btnGetUnSendMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetUnSendMsg();
            lbox.Items.Add("获取未发短信成功，总共" + msg.Count + "条");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    lbox.Items.Add(s);
                }
            }
        }

        private void btnGetIsReadMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetIsReadMsg();
            lbox.Items.Add("获取已读短信成功，总共" + msg.Count + "条");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    lbox.Items.Add(s);
                }
            }
        }

        private void btnGetIsSendMsg_Click(object sender, EventArgs e)
        {
            List<string> msg = gsm.GetIsSendMsg();
            lbox.Items.Add("获取已发短信成功，总共" + msg.Count + "条");
            if (msg.Count != 0)
            {
                foreach (string s in msg)
                {
                    lbox.Items.Add(s);
                }
            }
        }

        private void btnGetMsgByIndex_Click(object sender, EventArgs e)
        {
            string msg = gsm.ReadMsgByIndex(Convert.ToInt32( txtxuhao1.Text.Trim()));
            lbox.Items.Add("获取指定序号短信成功");            
            if (msg.Length!=0)
            {
                lbox.Items.Add(msg);
            }
        }

        private void btnDeleteMsgByIndex_Click(object sender, EventArgs e)
        {
            if (gsm.DelMsgByIndex(Convert.ToInt32(txtxuhao2.Text.Trim())))
            {
                lbox.Items.Add("删除指定序号短信成功");
            }
            else
            {
                lbox.Items.Add("删除指定序号短信失败");
            }
        }

        private void btnSendAT_Click(object sender, EventArgs e)
        {
            lbox.Items.Add("发送AT命令成功");
            lbox.Items.Add("返回:" + gsm.SendAT(txtAT.Text.Trim()));
        }

        private void btnCheckConn_Click(object sender, EventArgs e)
        {
            if (gsm.isConnect())
            {
                lbox.Items.Add("连接成功");
            }
            else
            {
                lbox.Items.Add("连接失败");
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (gsm.DelAllMsg())
            {
                lbox.Items.Add("删除所有短信成功");
            }
            else
            {
                lbox.Items.Add("删除所有短信失败");
            }
        }

        private void btnIsAutoDel_Click(object sender, EventArgs e)
        {
            lbox.Items.Add("是否自动删除:" + gsm.AutoDelMsg.ToString());            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.GetCurrentProcess().MaxWorkingSet = new IntPtr(750000);
        }
    }
}