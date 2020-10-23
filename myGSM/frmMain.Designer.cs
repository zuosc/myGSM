namespace myGSM
{
    partial class frmMain
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.btnOpen = new System.Windows.Forms.Button();
            this.btnGetMsgByIndex = new System.Windows.Forms.Button();
            this.btnGetAllMsg = new System.Windows.Forms.Button();
            this.txtReceiveMsg = new System.Windows.Forms.TextBox();
            this.btnGetUnReadMsg = new System.Windows.Forms.Button();
            this.btnGetNum = new System.Windows.Forms.Button();
            this.btnGetJQM = new System.Windows.Forms.Button();
            this.btnSendMsg = new System.Windows.Forms.Button();
            this.btnCall = new System.Windows.Forms.Button();
            this.gbox1 = new System.Windows.Forms.GroupBox();
            this.getIMEI = new System.Windows.Forms.Button();
            this.clearButton = new System.Windows.Forms.Button();
            this.btnIsAutoDel = new System.Windows.Forms.Button();
            this.txtAT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtxuhao2 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtxuhao1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboxPort = new System.Windows.Forms.ComboBox();
            this.btnSendAT = new System.Windows.Forms.Button();
            this.btnDeleteAll = new System.Windows.Forms.Button();
            this.btnCheckConn = new System.Windows.Forms.Button();
            this.btnDeleteMsgByIndex = new System.Windows.Forms.Button();
            this.btnGetIsSendMsg = new System.Windows.Forms.Button();
            this.btnGetIsReadMsg = new System.Windows.Forms.Button();
            this.btnGetUnSendMsg = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.gbox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.gbox3 = new System.Windows.Forms.GroupBox();
            this.rbtnPDU = new System.Windows.Forms.RadioButton();
            this.rbtnText = new System.Windows.Forms.RadioButton();
            this.txtContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTel = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gbox4 = new System.Windows.Forms.GroupBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gbox1.SuspendLayout();
            this.gbox2.SuspendLayout();
            this.gbox3.SuspendLayout();
            this.gbox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOpen
            // 
            this.btnOpen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOpen.Location = new System.Drawing.Point(158, 20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(87, 23);
            this.btnOpen.TabIndex = 31;
            this.btnOpen.Text = "打开设备";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnGetMsgByIndex
            // 
            this.btnGetMsgByIndex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetMsgByIndex.Location = new System.Drawing.Point(158, 351);
            this.btnGetMsgByIndex.Name = "btnGetMsgByIndex";
            this.btnGetMsgByIndex.Size = new System.Drawing.Size(143, 23);
            this.btnGetMsgByIndex.TabIndex = 30;
            this.btnGetMsgByIndex.Text = "读取指定序号短信";
            this.btnGetMsgByIndex.UseVisualStyleBackColor = true;
            this.btnGetMsgByIndex.Click += new System.EventHandler(this.btnGetMsgByIndex_Click);
            // 
            // btnGetAllMsg
            // 
            this.btnGetAllMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetAllMsg.Location = new System.Drawing.Point(158, 116);
            this.btnGetAllMsg.Name = "btnGetAllMsg";
            this.btnGetAllMsg.Size = new System.Drawing.Size(143, 23);
            this.btnGetAllMsg.TabIndex = 29;
            this.btnGetAllMsg.Text = "获取所有短信";
            this.btnGetAllMsg.UseVisualStyleBackColor = true;
            this.btnGetAllMsg.Click += new System.EventHandler(this.btnGetAllMsg_Click);
            // 
            // txtReceiveMsg
            // 
            this.txtReceiveMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReceiveMsg.Location = new System.Drawing.Point(3, 17);
            this.txtReceiveMsg.Multiline = true;
            this.txtReceiveMsg.Name = "txtReceiveMsg";
            this.txtReceiveMsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtReceiveMsg.Size = new System.Drawing.Size(784, 98);
            this.txtReceiveMsg.TabIndex = 28;
            // 
            // btnGetUnReadMsg
            // 
            this.btnGetUnReadMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetUnReadMsg.Location = new System.Drawing.Point(9, 116);
            this.btnGetUnReadMsg.Name = "btnGetUnReadMsg";
            this.btnGetUnReadMsg.Size = new System.Drawing.Size(143, 23);
            this.btnGetUnReadMsg.TabIndex = 27;
            this.btnGetUnReadMsg.Text = "获取未读短信";
            this.btnGetUnReadMsg.UseVisualStyleBackColor = true;
            this.btnGetUnReadMsg.Click += new System.EventHandler(this.btnGetUnReadMsg_Click);
            // 
            // btnGetNum
            // 
            this.btnGetNum.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetNum.Location = new System.Drawing.Point(9, 87);
            this.btnGetNum.Name = "btnGetNum";
            this.btnGetNum.Size = new System.Drawing.Size(143, 23);
            this.btnGetNum.TabIndex = 25;
            this.btnGetNum.Text = "获取服务中心号码";
            this.btnGetNum.UseVisualStyleBackColor = true;
            this.btnGetNum.Click += new System.EventHandler(this.btnGetNum_Click);
            // 
            // btnGetJQM
            // 
            this.btnGetJQM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetJQM.Location = new System.Drawing.Point(9, 58);
            this.btnGetJQM.Name = "btnGetJQM";
            this.btnGetJQM.Size = new System.Drawing.Size(143, 23);
            this.btnGetJQM.TabIndex = 24;
            this.btnGetJQM.Text = "获取机器码";
            this.btnGetJQM.UseVisualStyleBackColor = true;
            this.btnGetJQM.Click += new System.EventHandler(this.btnGetJQM_Click);
            // 
            // btnSendMsg
            // 
            this.btnSendMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendMsg.Location = new System.Drawing.Point(533, 18);
            this.btnSendMsg.Name = "btnSendMsg";
            this.btnSendMsg.Size = new System.Drawing.Size(105, 23);
            this.btnSendMsg.TabIndex = 23;
            this.btnSendMsg.Text = "发送短信";
            this.btnSendMsg.UseVisualStyleBackColor = true;
            this.btnSendMsg.Click += new System.EventHandler(this.btnSendMsg_Click);
            // 
            // btnCall
            // 
            this.btnCall.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCall.Location = new System.Drawing.Point(422, 18);
            this.btnCall.Name = "btnCall";
            this.btnCall.Size = new System.Drawing.Size(105, 23);
            this.btnCall.TabIndex = 22;
            this.btnCall.Text = "拨打电话";
            this.btnCall.UseVisualStyleBackColor = true;
            this.btnCall.Click += new System.EventHandler(this.btnCall_Click);
            // 
            // gbox1
            // 
            this.gbox1.Controls.Add(this.getIMEI);
            this.gbox1.Controls.Add(this.clearButton);
            this.gbox1.Controls.Add(this.btnIsAutoDel);
            this.gbox1.Controls.Add(this.txtAT);
            this.gbox1.Controls.Add(this.label6);
            this.gbox1.Controls.Add(this.txtxuhao2);
            this.gbox1.Controls.Add(this.label5);
            this.gbox1.Controls.Add(this.txtxuhao1);
            this.gbox1.Controls.Add(this.label4);
            this.gbox1.Controls.Add(this.label3);
            this.gbox1.Controls.Add(this.cboxPort);
            this.gbox1.Controls.Add(this.btnSendAT);
            this.gbox1.Controls.Add(this.btnDeleteAll);
            this.gbox1.Controls.Add(this.btnCheckConn);
            this.gbox1.Controls.Add(this.btnDeleteMsgByIndex);
            this.gbox1.Controls.Add(this.btnGetIsSendMsg);
            this.gbox1.Controls.Add(this.btnGetIsReadMsg);
            this.gbox1.Controls.Add(this.btnGetUnSendMsg);
            this.gbox1.Controls.Add(this.labStatus);
            this.gbox1.Controls.Add(this.btnOpen);
            this.gbox1.Controls.Add(this.btnGetMsgByIndex);
            this.gbox1.Controls.Add(this.btnGetAllMsg);
            this.gbox1.Controls.Add(this.btnGetUnReadMsg);
            this.gbox1.Controls.Add(this.btnGetNum);
            this.gbox1.Controls.Add(this.btnGetJQM);
            this.gbox1.Location = new System.Drawing.Point(12, 12);
            this.gbox1.Name = "gbox1";
            this.gbox1.Size = new System.Drawing.Size(315, 382);
            this.gbox1.TabIndex = 33;
            this.gbox1.TabStop = false;
            this.gbox1.Text = "操作区";
            // 
            // getIMEI
            // 
            this.getIMEI.Location = new System.Drawing.Point(7, 203);
            this.getIMEI.Name = "getIMEI";
            this.getIMEI.Size = new System.Drawing.Size(145, 23);
            this.getIMEI.TabIndex = 53;
            this.getIMEI.Text = "获取IMEI";
            this.getIMEI.UseVisualStyleBackColor = true;
            this.getIMEI.Click += new System.EventHandler(this.getIMEI_Click);
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(158, 254);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(143, 33);
            this.clearButton.TabIndex = 37;
            this.clearButton.Text = "清除信息";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // btnIsAutoDel
            // 
            this.btnIsAutoDel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIsAutoDel.Location = new System.Drawing.Point(158, 145);
            this.btnIsAutoDel.Name = "btnIsAutoDel";
            this.btnIsAutoDel.Size = new System.Drawing.Size(143, 23);
            this.btnIsAutoDel.TabIndex = 52;
            this.btnIsAutoDel.Text = "检测是否自动删除";
            this.btnIsAutoDel.UseVisualStyleBackColor = true;
            this.btnIsAutoDel.Click += new System.EventHandler(this.btnIsAutoDel_Click);
            // 
            // txtAT
            // 
            this.txtAT.Location = new System.Drawing.Point(52, 295);
            this.txtAT.Name = "txtAT";
            this.txtAT.Size = new System.Drawing.Size(100, 21);
            this.txtAT.TabIndex = 51;
            this.txtAT.Text = "AT+CMGF=0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 298);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 12);
            this.label6.TabIndex = 50;
            this.label6.Text = "命令:";
            // 
            // txtxuhao2
            // 
            this.txtxuhao2.Location = new System.Drawing.Point(52, 324);
            this.txtxuhao2.Name = "txtxuhao2";
            this.txtxuhao2.Size = new System.Drawing.Size(100, 21);
            this.txtxuhao2.TabIndex = 49;
            this.txtxuhao2.Text = "1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 48;
            this.label5.Text = "序号:";
            // 
            // txtxuhao1
            // 
            this.txtxuhao1.Location = new System.Drawing.Point(52, 353);
            this.txtxuhao1.Name = "txtxuhao1";
            this.txtxuhao1.Size = new System.Drawing.Size(100, 21);
            this.txtxuhao1.TabIndex = 47;
            this.txtxuhao1.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 356);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 46;
            this.label4.Text = "序号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 12);
            this.label3.TabIndex = 45;
            this.label3.Text = "串口:";
            // 
            // cboxPort
            // 
            this.cboxPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxPort.FormattingEnabled = true;
            this.cboxPort.Location = new System.Drawing.Point(52, 23);
            this.cboxPort.Name = "cboxPort";
            this.cboxPort.Size = new System.Drawing.Size(100, 20);
            this.cboxPort.TabIndex = 44;
            // 
            // btnSendAT
            // 
            this.btnSendAT.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSendAT.Location = new System.Drawing.Point(158, 293);
            this.btnSendAT.Name = "btnSendAT";
            this.btnSendAT.Size = new System.Drawing.Size(143, 23);
            this.btnSendAT.TabIndex = 43;
            this.btnSendAT.Text = "发送AT命令";
            this.btnSendAT.UseVisualStyleBackColor = true;
            this.btnSendAT.Click += new System.EventHandler(this.btnSendAT_Click);
            // 
            // btnDeleteAll
            // 
            this.btnDeleteAll.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteAll.Location = new System.Drawing.Point(158, 173);
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.Size = new System.Drawing.Size(143, 23);
            this.btnDeleteAll.TabIndex = 42;
            this.btnDeleteAll.Text = "删除所有短信";
            this.btnDeleteAll.UseVisualStyleBackColor = true;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // btnCheckConn
            // 
            this.btnCheckConn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckConn.Location = new System.Drawing.Point(9, 173);
            this.btnCheckConn.Name = "btnCheckConn";
            this.btnCheckConn.Size = new System.Drawing.Size(143, 23);
            this.btnCheckConn.TabIndex = 41;
            this.btnCheckConn.Text = "检测设备连接";
            this.btnCheckConn.UseVisualStyleBackColor = true;
            this.btnCheckConn.Click += new System.EventHandler(this.btnCheckConn_Click);
            // 
            // btnDeleteMsgByIndex
            // 
            this.btnDeleteMsgByIndex.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDeleteMsgByIndex.Location = new System.Drawing.Point(158, 322);
            this.btnDeleteMsgByIndex.Name = "btnDeleteMsgByIndex";
            this.btnDeleteMsgByIndex.Size = new System.Drawing.Size(143, 23);
            this.btnDeleteMsgByIndex.TabIndex = 39;
            this.btnDeleteMsgByIndex.Text = "删除指定序号短信";
            this.btnDeleteMsgByIndex.UseVisualStyleBackColor = true;
            this.btnDeleteMsgByIndex.Click += new System.EventHandler(this.btnDeleteMsgByIndex_Click);
            // 
            // btnGetIsSendMsg
            // 
            this.btnGetIsSendMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetIsSendMsg.Location = new System.Drawing.Point(158, 87);
            this.btnGetIsSendMsg.Name = "btnGetIsSendMsg";
            this.btnGetIsSendMsg.Size = new System.Drawing.Size(143, 23);
            this.btnGetIsSendMsg.TabIndex = 35;
            this.btnGetIsSendMsg.Text = "获取已发短信";
            this.btnGetIsSendMsg.UseVisualStyleBackColor = true;
            this.btnGetIsSendMsg.Click += new System.EventHandler(this.btnGetIsSendMsg_Click);
            // 
            // btnGetIsReadMsg
            // 
            this.btnGetIsReadMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetIsReadMsg.Location = new System.Drawing.Point(9, 145);
            this.btnGetIsReadMsg.Name = "btnGetIsReadMsg";
            this.btnGetIsReadMsg.Size = new System.Drawing.Size(143, 23);
            this.btnGetIsReadMsg.TabIndex = 34;
            this.btnGetIsReadMsg.Text = "获取已读短信";
            this.btnGetIsReadMsg.UseVisualStyleBackColor = true;
            this.btnGetIsReadMsg.Click += new System.EventHandler(this.btnGetIsReadMsg_Click);
            // 
            // btnGetUnSendMsg
            // 
            this.btnGetUnSendMsg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGetUnSendMsg.Location = new System.Drawing.Point(158, 59);
            this.btnGetUnSendMsg.Name = "btnGetUnSendMsg";
            this.btnGetUnSendMsg.Size = new System.Drawing.Size(143, 23);
            this.btnGetUnSendMsg.TabIndex = 33;
            this.btnGetUnSendMsg.Text = "获取未发短信";
            this.btnGetUnSendMsg.UseVisualStyleBackColor = true;
            this.btnGetUnSendMsg.Click += new System.EventHandler(this.btnGetUnSendMsg_Click);
            // 
            // labStatus
            // 
            this.labStatus.BackColor = System.Drawing.Color.LemonChiffon;
            this.labStatus.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labStatus.ForeColor = System.Drawing.Color.Red;
            this.labStatus.Location = new System.Drawing.Point(251, 20);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(50, 20);
            this.labStatus.TabIndex = 32;
            this.labStatus.Text = "未连接";
            this.labStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gbox2
            // 
            this.gbox2.Controls.Add(this.richTextBox);
            this.gbox2.Location = new System.Drawing.Point(333, 12);
            this.gbox2.Name = "gbox2";
            this.gbox2.Size = new System.Drawing.Size(475, 382);
            this.gbox2.TabIndex = 34;
            this.gbox2.TabStop = false;
            this.gbox2.Text = "信息显示";
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(6, 14);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(463, 362);
            this.richTextBox.TabIndex = 0;
            this.richTextBox.Text = "";
            // 
            // gbox3
            // 
            this.gbox3.Controls.Add(this.rbtnPDU);
            this.gbox3.Controls.Add(this.rbtnText);
            this.gbox3.Controls.Add(this.txtContent);
            this.gbox3.Controls.Add(this.label2);
            this.gbox3.Controls.Add(this.txtTel);
            this.gbox3.Controls.Add(this.label1);
            this.gbox3.Controls.Add(this.btnCall);
            this.gbox3.Controls.Add(this.btnSendMsg);
            this.gbox3.Location = new System.Drawing.Point(14, 400);
            this.gbox3.Name = "gbox3";
            this.gbox3.Size = new System.Drawing.Size(788, 76);
            this.gbox3.TabIndex = 35;
            this.gbox3.TabStop = false;
            this.gbox3.Text = "发送短信+拨打电话";
            // 
            // rbtnPDU
            // 
            this.rbtnPDU.AutoSize = true;
            this.rbtnPDU.Checked = true;
            this.rbtnPDU.Location = new System.Drawing.Point(326, 24);
            this.rbtnPDU.Name = "rbtnPDU";
            this.rbtnPDU.Size = new System.Drawing.Size(89, 16);
            this.rbtnPDU.TabIndex = 25;
            this.rbtnPDU.TabStop = true;
            this.rbtnPDU.Text = "PDU(中英文)";
            this.rbtnPDU.UseVisualStyleBackColor = true;
            // 
            // rbtnText
            // 
            this.rbtnText.AutoSize = true;
            this.rbtnText.Location = new System.Drawing.Point(237, 25);
            this.rbtnText.Name = "rbtnText";
            this.rbtnText.Size = new System.Drawing.Size(83, 16);
            this.rbtnText.TabIndex = 24;
            this.rbtnText.Text = "Text(英文)";
            this.rbtnText.UseVisualStyleBackColor = true;
            // 
            // txtContent
            // 
            this.txtContent.Location = new System.Drawing.Point(50, 47);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(588, 21);
            this.txtContent.TabIndex = 3;
            this.txtContent.Text = "我就来测试短信哦";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "内容:";
            // 
            // txtTel
            // 
            this.txtTel.Location = new System.Drawing.Point(118, 20);
            this.txtTel.Name = "txtTel";
            this.txtTel.Size = new System.Drawing.Size(100, 21);
            this.txtTel.TabIndex = 1;
            this.txtTel.Text = "+1 2132422281";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "号码(带上国家码):";
            // 
            // gbox4
            // 
            this.gbox4.Controls.Add(this.txtReceiveMsg);
            this.gbox4.Location = new System.Drawing.Point(12, 490);
            this.gbox4.Name = "gbox4";
            this.gbox4.Size = new System.Drawing.Size(790, 118);
            this.gbox4.TabIndex = 36;
            this.gbox4.TabStop = false;
            this.gbox4.Text = "接收短信";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(820, 620);
            this.Controls.Add(this.gbox4);
            this.Controls.Add(this.gbox3);
            this.Controls.Add(this.gbox2);
            this.Controls.Add(this.gbox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "短信猫调试工具V1.0(作者：刘典武)";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.gbox1.ResumeLayout(false);
            this.gbox1.PerformLayout();
            this.gbox2.ResumeLayout(false);
            this.gbox3.ResumeLayout(false);
            this.gbox3.PerformLayout();
            this.gbox4.ResumeLayout(false);
            this.gbox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnGetMsgByIndex;
        private System.Windows.Forms.Button btnGetAllMsg;
        private System.Windows.Forms.TextBox txtReceiveMsg;
        private System.Windows.Forms.Button btnGetUnReadMsg;
        private System.Windows.Forms.Button btnGetNum;
        private System.Windows.Forms.Button btnGetJQM;
        private System.Windows.Forms.Button btnSendMsg;
        private System.Windows.Forms.Button btnCall;
        private System.Windows.Forms.GroupBox gbox1;
        private System.Windows.Forms.GroupBox gbox2;
        private System.Windows.Forms.GroupBox gbox3;
        private System.Windows.Forms.TextBox txtContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbox4;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Button btnGetUnSendMsg;
        private System.Windows.Forms.Button btnGetIsReadMsg;
        private System.Windows.Forms.Button btnGetIsSendMsg;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboxPort;
        private System.Windows.Forms.Button btnSendAT;
        private System.Windows.Forms.Button btnDeleteAll;
        private System.Windows.Forms.Button btnCheckConn;
        private System.Windows.Forms.Button btnDeleteMsgByIndex;
        private System.Windows.Forms.RadioButton rbtnPDU;
        private System.Windows.Forms.RadioButton rbtnText;
        private System.Windows.Forms.TextBox txtxuhao1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtxuhao2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnIsAutoDel;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button getIMEI;
    }
}

