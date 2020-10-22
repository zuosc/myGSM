//类名：myGSMModem
//作用：短信猫类
//作者：刘典武修正（通用于深圳百亿科技GSM）
//时间：2011-03-01
//说明：来源于博客园(修正)

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO.Ports;

namespace myGSM
{
    public enum MsgType { AUSC2, A7Bit };//枚举 短信类型 AUSC2 A7Bit：7位编码 （中文用AUSC2，英文都可以 但7Bit能发送160字符，USC2仅70）
    public class myGSMModem
    {
        #region  构造函数
        /// <summary>
        /// 无参数构造函数 完成有关初始化工作
        /// </summary>
        public myGSMModem()
        {            
            this.msgCenter = string.Empty;
            this.sp = new SerialPort();
            this.sp.ReadTimeout = 5000;//读超时时间 发送短信时间的需要
            this.sp.RtsEnable = true;//必须为true 这样串口才能接收到数据
            this.sp.DataReceived += new SerialDataReceivedEventHandler(this.sp_DataReceived);
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comPort">串口号</param>
        /// <param name="baudRate">波特率</param>
        public myGSMModem(string comPort, int baudRate)
        {            
            this.msgCenter = string.Empty;
            this.sp = new SerialPort();
            this.sp.PortName = comPort;
            this.sp.BaudRate = baudRate;
            this.sp.ReadTimeout = 5000;//读超时时间 发送短信时间的需要
            this.sp.RtsEnable = true;//必须为true 这样串口才能接收到数据
            this.sp.DataReceived += new SerialDataReceivedEventHandler(this.sp_DataReceived);
        }
        #endregion

        #region  私有变量
        private SerialPort sp;
        private bool autoDelMsg = false;//是否自动删除短消息
        private string msgCenter = string.Empty;//服务中心号码
        private int newMsgIndex;//新消息序号        
        #endregion

        #region 属性
        /// <summary>
        /// 是否自动删除短信(默认为false)
        /// </summary>
        public bool AutoDelMsg
        {
            get { return this.autoDelMsg; }
            set { this.autoDelMsg = value; }
        }

        /// <summary>
        /// 波特率 运行时只读 设备打开状态写入将引发异常
        /// </summary>
        public int BaudRate
        {
            get { return this.sp.BaudRate; }
            set { this.sp.BaudRate = value; }
        }

        /// <summary>
        /// 串口号 运行时只读 设备打开状态写入将引发异常
        /// 提供对串口端口号的访问
        /// </summary>
        public string ComPort
        {
            get { return this.sp.PortName; }
            set { try { this.sp.PortName = value; } catch  { } }
        }

        /// <summary>
        /// 设备是否打开
        /// </summary>
        public bool IsOpen
        {
            get { return this.sp.IsOpen; }
        }

        #endregion

        #region  方法

        /// <summary>
        /// 设置服务中心号码
        /// </summary>
        /// <param name="msgCenterNo">服务中心号码</param>
        public void SetMsgCenterNo(string msgCenterNo)
        {
            this.msgCenter = msgCenterNo;
        }

        /// <summary>
        /// 设置是否自动删除新消息
        /// </summary>
        /// <param name="b">True则自动删除</param>
        public void SetAutoDelMsg(bool b)
        {
            this.autoDelMsg = b;
        }

        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <returns></returns>
        public string GetMachineNo()
        {
            string temp=string.Empty;
            try
            {
                temp = this.SendAT("AT+CGMI");
                if (temp.Substring(temp.Length - 4, 3).Trim() == "OK")
                {
                    temp = temp.Split('\r')[2].Trim();
                }
                else
                {
                    temp = "获取机器码失败";
                }
                return temp;
            }
            catch { return "获取机器码失败"; }
            
        }

        /// <summary>
        /// 获取短信中心号码
        /// </summary>
        /// <returns></returns>
        public string GetMsgCenterNo()
        {
            string temp = string.Empty;
            try
            {
                if (msgCenter != null && msgCenter.Length != 0)
                {
                    return msgCenter;
                }
                else
                {
                    temp = this.SendAT("AT+CSCA?");
                    if (temp.Substring(temp.Length - 4, 3).Trim() == "OK")
                    {
                        temp = temp.Split('\"')[1].Trim();
                    }
                    else
                    {
                        temp = "获取短信中心失败";
                    }
                    return temp;
                }
            }
            catch { return "获取短信中心失败"; }
        }


        /// <summary>
        /// 获取短信中心号码
        /// </summary>
        /// <returns></returns>
        public string GeIMEINo()
        {
            string temp = string.Empty;
            try
            {
                if (msgCenter != null && msgCenter.Length != 0)
                {
                    return msgCenter;
                }
                else
                {
                    temp = this.SendAT("AT+CGSN");
                    if (temp.Substring(temp.Length - 4, 3).Trim() == "OK")
                    {
                        temp = temp.Substring(0, 16).Trim();
                    }
                    else
                    {
                        temp = "获取IMEI失败";
                    }
                    return temp;
                }
            }
            catch { return "获取IMEI失败"; }
        }

        /// <summary>
        /// 取得未读信息列表
        /// </summary>
        /// <returns>未读信息列表（中心号码，手机号码，发送时间，短信内容）</returns>
        public List<string> GetUnReadMsg()
        {
            List<string> myResult = new List<string>();
            string[] temp = null;
            string tt = string.Empty;

            tt = this.SendAT("AT+CMGL=0");//读取未读信息
            if (tt.Substring(tt.Length - 4, 3).Trim() == "OK")
            {
                temp = tt.Split('\r');
            }
            PDUEncoding pe = new PDUEncoding();
            foreach (string str in temp)
            {
                if (str != null && str.Length != 0 && str.Substring(0, 2).Trim() != "+C" && str.Substring(0, 2) != "OK" && str.Substring(0, 2) != "AT")
                {
                    myResult.Add(pe.PDUDecoder(str));
                }
            }
            return myResult;
        }

        /// <summary>
        /// 取得已读信息列表
        /// </summary>
        /// <returns>已读信息列表（中心号码，手机号码，发送时间，短信内容）</returns>
        public List<string> GetIsReadMsg()
        {
            List<string> myResult = new List<string>();
            string[] temp = null;
            string tt = string.Empty;

            tt = this.SendAT("AT+CMGL=1");//读取已读信息
            if (tt.Substring(tt.Length - 4, 3).Trim() == "OK")
            {
                temp = tt.Split('\r');
            }
            PDUEncoding pe = new PDUEncoding();
            foreach (string str in temp)
            {
                if (str != null && str.Length != 0 && str.Substring(0, 2).Trim() != "+C" && str.Substring(0, 2) != "OK" && str.Substring(0, 2) != "AT")
                {
                    myResult.Add(pe.PDUDecoder(str));
                }
            }
            return myResult;           
        }

        /// <summary>
        /// 取得待发信息列表
        /// </summary>
        /// <returns>待发信息列表（中心号码，手机号码，发送时间，短信内容）</returns>
        public List<string> GetUnSendMsg()
        {
            List<string> myResult = new List<string>();
            string[] temp = null;
            string tt = string.Empty;

            tt = this.SendAT("AT+CMGL=2");//读取待发信息
            if (tt.Substring(tt.Length - 4, 3).Trim() == "OK")
            {
                temp = tt.Split('\r');
            }
            PDUEncoding pe = new PDUEncoding();
            foreach (string str in temp)
            {
                if (str != null && str.Length != 0 && str.Substring(0, 2).Trim() != "+C" && str.Substring(0, 2) != "OK" && str.Substring(0, 2) != "AT")
                {
                    myResult.Add(pe.PDUDecoder(str));
                }
            }
            return myResult;
        }

        /// <summary>
        /// 取得已发信息列表
        /// </summary>
        /// <returns>已发信息列表（中心号码，手机号码，发送时间，短信内容）</returns>
        public List<string> GetIsSendMsg()
        {
            List<string> myResult = new List<string>();
            string[] temp = null;
            string tt = string.Empty;

            tt = this.SendAT("AT+CMGL=3");//读取已发信息
            if (tt.Substring(tt.Length - 4, 3).Trim() == "OK")
            {
                temp = tt.Split('\r');
            }
            PDUEncoding pe = new PDUEncoding();
            foreach (string str in temp)
            {
                if (str != null && str.Length != 0 && str.Substring(0, 2).Trim() != "+C" && str.Substring(0, 2) != "OK" && str.Substring(0, 2) != "AT")
                {
                    myResult.Add(pe.PDUDecoder(str));
                }
            }
            return myResult;
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMsg()
        {
            List<string> myResult = new List<string>();
            string[] temp = null;
            string tt = string.Empty;

            tt = this.SendAT("AT+CMGL=4");//读取所有信息
            if (tt.Substring(tt.Length - 4, 3).Trim() == "OK")
            {
                temp = tt.Split('\r');
            }
            PDUEncoding pe = new PDUEncoding();
            foreach (string str in temp)
            {
                if (str != null && str.Length != 0 && str.Substring(0, 2).Trim() != "+C" && str.Substring(0, 2) != "OK" && str.Substring(0, 2) != "AT")
                {
                    myResult.Add(pe.PDUDecoder(str));
                }
            }
            return myResult;
        }

        /// <summary>
        /// 按序号读取短信（Text模式）
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string ReadMsgInText(int index)
        {
            string temp = string.Empty;
            try
            {
                this.SendAT("AT+CMGF=1");
                Thread.Sleep(100);
                temp = this.SendAT("AT+CMGR=" + index.ToString());
                temp = temp.Split((char)(13))[3];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串            

                if (AutoDelMsg)//如果阅读完短信自动删除设置为真
                {
                    try
                    {
                        DelMsgByIndex(index);
                    }
                    catch { }
                }
                return temp;
            }
            catch { return "读取短信失败"; }
        }

        /// <summary>
        /// 读取设备新收到的短消息（Text模式）
        /// </summary>
        /// <returns></returns>
        public string ReadNewMsgInText()
        {
            return ReadMsgInText(newMsgIndex);
        }

        /// <summary>
        /// 读取设备新收到的短消息
        /// </summary>
        /// <returns></returns>
        public string ReadNewMsg()
        {
            return ReadMsgByIndex(newMsgIndex);
        }

        /// <summary>
        /// 读取设备新收到的短消息仅返回内容
        /// </summary>
        /// <returns></returns>
        public string ReadNewMsgOnlyContent()
        {
            return ReadOnlyMsgByIndex(newMsgIndex);
        }

        /// <summary>
        /// 发送AT命令
        /// </summary>
        /// <param name="ATCom">AT命令</param>
        /// <returns></returns>
        public string SendAT(string ATCom)
        {
            string str = string.Empty;
            //忽略接收缓冲区内容，准备发送
            this.sp.DiscardInBuffer();
            //注销事件关联，为发送做准备
            this.sp.DataReceived -= this.sp_DataReceived;
            try
            {
                this.sp.Write(ATCom + "\r");
            }
            catch {this.sp.DataReceived += this.sp_DataReceived;}
            try
            {
                string temp = string.Empty;
                while ((temp.Trim() != "OK") && (temp.Trim() != "ERROR"))
                {
                    temp = this.sp.ReadLine();
                    str += temp;
                }
            }
            catch { }//遇到错误不作处理            
            finally
            {
                this.sp.DataReceived += this.sp_DataReceived;
            }
            return str;
        }

        /// <summary>
        /// 检测短信猫连接是否成功
        /// </summary>
        /// <returns>成功返回真，否则返回假</returns>
        public bool isConnect()
        {
            string temp = "";
            try
            {
                temp = this.SendAT("AT");
                if (temp.Substring(temp.Length - 4, 3).Trim() != "OK")
                {
                    return false;
                }
            }
            catch { return false; }
            return true;
        }

        /// <summary>
        /// 发送短信PDU模式       
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="msg">短信内容</param>
        public bool SendMsg(string phone, string msg)
        {
            string temp = "0011000D91" + this.reverserNumber(phone) + "000801" + this.contentEncoding(msg) + Convert.ToChar(26).ToString();
            string len = this.getLenght(msg);//计算长度

            try
            {
                this.sp.DataReceived -= sp_DataReceived;
                this.sp.Write("AT+CMGF=0" + "\r");//设置为PDU模式
                Thread.Sleep(100);
                this.sp.Write("AT+CMGS=" + len + "\r");
                this.sp.ReadTo(">");
                this.sp.DiscardInBuffer();
                //事件重新绑定 正常监视串口数据
                this.sp.DataReceived += sp_DataReceived;
                temp = this.SendAT(temp);
                if (temp.Substring(temp.Length - 4, 3).Trim() != "OK")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }

        /// <summary>
        /// 发送短信Text模式       
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="msg">短信内容</param>
        public bool SendMsgText(string phone, string msg)
        {
            try
            {
                string temp = msg + Convert.ToChar(26).ToString();
                this.sp.DataReceived -= sp_DataReceived;
                this.sp.Write("AT+CMGF=1" + "\r");//设置为text模式
                Thread.Sleep(200);
                this.sp.Write("AT+CMGS=" + phone + "\r");
                this.sp.ReadTo(">");
                this.sp.DiscardInBuffer();
                //事件重新绑定 正常监视串口数据
                this.sp.DataReceived += sp_DataReceived;
                temp = this.SendAT(temp);
                if (temp.Substring(temp.Length - 4, 3).Trim() != "OK")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch { return false; }
        }

        #region  自定义方法

        //获取短信内容的字节数
        private string getLenght(string txt)
        {
            int i = 0;
            string s = "";
            i = txt.Length * 2;
            i += 15;
            s = i.ToString();
            return s;
        }
        //将手机号码转换为内存编码
        private string reverserNumber(string phone)
        {
            string str = "";
            //检查手机号码是否按照标准格式写，如果不是则补上
            if (phone.Substring(0, 2) != "86")
            {
                phone = string.Format("86{0}", phone);
            }
            char[] c = this.getChar(phone);
            for (int i = 0; i <= c.Length - 2; i += 2)
            {
                str += c[i + 1].ToString() + c[i].ToString();
            }
            return str;
        }
        //汉字解码为16进制
        private string contentEncoding(string content)
        {
            Encoding encodingUTF = System.Text.Encoding.BigEndianUnicode;
            string s = "";
            byte[] encodeByte = encodingUTF.GetBytes(content);
            for (int i = 0; i <= encodeByte.Length - 1; i++)
            {
                s += BitConverter.ToString(encodeByte, i, 1);
            }
            s = string.Format("{0:X2}{1}", s.Length / 2, s);
            return s;
        }

        private char[] getChar(string phone)
        {
            if (phone.Length % 2 == 0)
            {
                return Convert.ToString(phone).ToCharArray();
            }
            else
            {
                return Convert.ToString(phone + "F").ToCharArray();
            }
        }
        #endregion


        /// <summary>
        /// 发送短信 （重载）
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="msg">短信内容</param>
        /// <param name="msgType">短信类型</param>
        public void SendMsg(string phone, string msg, MsgType msgType)
        {
            if (msgType == MsgType.AUSC2)
            {
                SendMsg(phone, msg);
            }
            else
            {

                PDUEncoding pe = new PDUEncoding();
                pe.ServiceCenterAddress = msgCenter;                    //短信中心号码 服务中心地址

                string temp = pe.PDU7BitEncoder(phone, msg);

                int len = (temp.Length - Convert.ToInt32(temp.Substring(0, 2), 16) * 2 - 2) / 2;  //计算长度
                try
                {
                    temp = SendAT("AT+CMGS=" + len.ToString() + "\r" + temp + (char)(26));  //26 Ctrl+Z ascii码
                }
                catch { }

                if (temp.Substring(temp.Length - 4, 3).Trim() == "OK")
                {
                    return;
                }

                throw new Exception("短信发送失败");
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        public bool CloseComm()
        {
            try
            {
                this.sp.Close();
            }
            catch { return false; } return true;
        }

        /// <summary>
        /// 打开设备
        /// </summary>
        public bool OpenComm()
        {
            try
            {
                this.sp.Open();
            }
            catch { return false; }
            if (this.sp.IsOpen)
            {
                try
                {
                    this.sp.DataReceived -= this.sp_DataReceived;
                    this.sp.Write("AT\r");
                    Thread.Sleep(100);
                    string s = this.sp.ReadExisting().Trim();
                    s = s.Substring(s.Length - 2, 2);
                    if (s != "OK")
                    {
                        return false;
                    }
                    this.SendAT("AT+CMGF=0");//选择短消息格式默认为PDU
                    Thread.Sleep(100);
                    this.SendAT("AT+CNMI=2,1");//选择当有新短消息来时提示方式
                    Thread.Sleep(100);
                }
                catch { return false; }
            }
            return true;
        }

        /// <summary>
        /// 拨打电话
        /// </summary>
        /// <param name="telNum">电话号码</param>
        public void Call(string telNum)
        {
            try
            {
                this.SendAT("ATD" + telNum + ";");
            }
            catch { }
        }

        /// <summary>
        /// 按序号读取短信
        /// </summary>
        /// <param name="index">序号</param>
        /// <param name="msgCenter">短信中心</param>
        /// <param name="phone">发送方手机号码</param>
        /// <param name="msg">短信内容</param>
        /// <param name="time">时间字符串</param>
        public void ReadMsgByIndex(int index, out string msgCenter, out string phone, out string msg, out string time)
        {
            string temp = string.Empty;
            PDUEncoding pe = new PDUEncoding();
            try
            {
                temp = SendAT("AT+CMGR=" + index.ToString() + "\r");
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (temp.Trim() == "ERROR")
            {
                throw new Exception("没有此短信");
            }
            temp = temp.Split((char)(13))[2];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串

            pe.PDUDecoder(temp, out msgCenter, out phone, out msg, out time);
        }

        /// <summary>
        /// 按序号读取短信
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>信息字符串 (中心号码，手机号码，发送时间，短信内容)</returns>
        public string ReadMsgByIndex(int index)
        {
            string temp = string.Empty;
            string msgCenter, phone, msg, time;
            PDUEncoding pe = new PDUEncoding();
            try
            {
                temp = SendAT("AT+CMGR=" + index.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (temp.Trim() == "ERROR")
            {
                throw new Exception("没有此短信");
            }

            temp = temp.Split((char)(13))[3];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串

            pe.PDUDecoder(temp, out msgCenter, out phone, out msg, out time);

            if (AutoDelMsg)//如果阅读完短信自动删除设置为真
            {
                try
                {
                    DelMsgByIndex(index);
                }
                catch { }
            }
            return msgCenter + "," + phone + "," + time + "," + msg;
        }

        /// <summary>
        /// 按序号读取短信(只返回短信内容)
        /// </summary>
        /// <param name="index">序号</param>
        /// <returns>短信内容</returns>
        public string ReadOnlyMsgByIndex(int index)
        {
            string temp = string.Empty;
            string msgCenter, phone, msg, time;
            PDUEncoding pe = new PDUEncoding();
            try
            {
                temp = SendAT("AT+CMGR=" + index.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (temp.Trim() == "ERROR")
            {
                throw new Exception("没有此短信");
            }

            temp = temp.Split((char)(13))[3];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串

            pe.PDUDecoder(temp, out msgCenter, out phone, out msg, out time);

            if (AutoDelMsg)//如果阅读完短信自动删除设置为真
            {
                try
                {
                    DelMsgByIndex(index);
                }
                catch { }
            }
            return msg;
        }

        /// <summary>
        /// 删除对应序号短信
        /// </summary>
        /// <param name="index">短信序号</param>
        /// <returns></returns>
        public bool DelMsgByIndex(int index)
        {
            if (SendAT("AT+CMGD=" + index.ToString()).Trim() == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除所有短信(有些短信猫不支持)
        /// </summary>
        /// <returns></returns>
        public bool DelAllMsg()
        {
            if (SendAT("AT+CMGD=1,4") == "OK")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        /// <summary>
        /// 创建事件收到信息的委托
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void OnRecievedHandler(object sender, EventArgs e);

        /// <summary>
        /// 收到短信息事件 OnRecieved 
        /// 收到短信将引发此事件
        /// </summary>
        public event OnRecievedHandler GetNewMsg;

        private void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string str = this.sp.ReadLine();
            if ((str.Length > 8) && (str.Substring(0, 6) == "+CMTI:"))
            {
                this.newMsgIndex = Convert.ToInt32(str.Split(',')[1]);
                this.GetNewMsg(this, e);
            }
        }
    }

    class PDUEncoding
    {
        private string serviceCenterAddress = "00";
        /// <summary>
        /// 消息服务中心(1-12个8位组)
        /// </summary>
        public string ServiceCenterAddress
        {
            get
            {
                int len = 2 * Convert.ToInt32(serviceCenterAddress.Substring(0, 2));
                string result = serviceCenterAddress.Substring(4, len - 2);

                result = ParityChange(result);
                result = result.TrimEnd('F', 'f');
                return result;
            }
            set                 //
            {
                if (value == null || value.Length == 0)      //号码为空
                {
                    serviceCenterAddress = "00";
                }
                else
                {
                    if (value[0] == '+')
                    {
                        value = value.TrimStart('+');
                    }
                    if (value.Substring(0, 2) != "86")
                    {
                        value = "86" + value;
                    }
                    value = "91" + ParityChange(value);
                    serviceCenterAddress = (value.Length / 2).ToString("X2") + value;
                }

            }
        }

        private string protocolDataUnitType = "11";
        /// <summary>
        /// 协议数据单元类型(1个8位组)
        /// </summary>
        public string ProtocolDataUnitType
        {
            set
            {

            }
            get
            {
                return "11";
            }
        }

        private string messageReference = "00";
        /// <summary>
        /// 所有成功的短信发送参考数目（0..255）
        /// (1个8位组)
        /// </summary>
        public string MessageReference
        {
            get
            {
                return "00";
            }
        }

        private string originatorAddress = "00";
        /// <summary>
        /// 发送方地址（手机号码）(2-12个8位组)
        /// </summary>
        public string OriginatorAddress
        {
            get
            {
                int len = Convert.ToInt32(originatorAddress.Substring(0, 2), 16);    //十六进制字符串转为整形数据
                string result = string.Empty;
                if (len % 2 == 1)       //号码长度是奇数，长度加1 编码时加了F
                {
                    len++;
                }
                result = originatorAddress.Substring(4, len);
                result = ParityChange(result).TrimEnd('F', 'f');    //奇偶互换，并去掉结尾F

                return result;
            }
        }

        private string destinationAddress = "00";
        /// <summary>
        /// 接收方地址（手机号码）(2-12个8位组)
        /// </summary>
        public string DestinationAddress
        {
            set
            {
                if (value == null || value.Length == 0)      //号码为空
                {
                    destinationAddress = "00";
                }
                else
                {
                    if (value[0] == '+')
                    {
                        value = value.TrimStart('+');
                    }
                    if (value.Substring(0, 2) == "86")
                    {
                        value = value.TrimStart('8', '6');
                    }
                    int len = value.Length;
                    value = ParityChange(value);

                    destinationAddress = len.ToString("X2") + "A1" + value;
                }
            }
        }

        private string protocolIdentifer = "00";
        /// <summary>
        /// 参数显示消息中心以何种方式处理消息内容
        /// （比如FAX,Voice）(1个8位组)
        /// </summary>
        public string ProtocolIdentifer
        {
            get
            {
                return protocolIdentifer;
            }
            set
            {

            }
        }

        private string dataCodingScheme = "08";     //暂时仅支持国内USC2编码
        /// <summary>
        /// 参数显示用户数据编码方案(1个8位组)
        /// </summary>
        public string DataCodingScheme
        {
            get
            {
                return dataCodingScheme;
            }
        }

        private string serviceCenterTimeStamp = "";
        /// <summary>
        /// 消息中心收到消息时的时间戳(7个8位组)
        /// </summary>
        public string ServiceCenterTimeStamp
        {
            get
            {
                string result = ParityChange(serviceCenterTimeStamp);
                result = "20" + result.Substring(0, 12);            //年加开始的“20”

                return result;
            }
        }

        private string validityPeriod = "C4";       //暂时固定有效期
        /// <summary>
        /// 短消息有效期(0,1,7个8位组)
        /// </summary>
        public string ValidityPeriod
        {
            get
            {
                return "C4";
            }
        }

        private string userDataLenghth = "";
        /// <summary>
        /// 用户数据长度(1个8位组)
        /// </summary>
        public string UserDataLenghth
        {
            get
            {
                return (userData.Length / 2).ToString("X2");
            }
        }

        private string userData = "";
        /// <summary>
        /// 用户数据(0-140个8位组)
        /// </summary>
        public string UserData
        {
            get
            {
                int len = Convert.ToInt32(userDataLenghth, 16) * 2;
                string result = string.Empty;

                if (dataCodingScheme == "08" || dataCodingScheme == "18")             //USC2编码
                {
                    //四个一组，每组译为一个USC2字符
                    for (int i = 0; i < len; i += 4)
                    {
                        string temp = userData.Substring(i, 4);

                        int byte1 = Convert.ToInt16(temp, 16);

                        result += ((char)byte1).ToString();
                    }
                }
                else
                {
                    result = PDU7bitDecoder(userData);
                }

                return result;
            }
            set
            {
                userData = string.Empty;
                Encoding encodingUTF = Encoding.BigEndianUnicode;

                byte[] Bytes = encodingUTF.GetBytes(value);

                for (int i = 0; i < Bytes.Length; i++)
                {
                    userData += BitConverter.ToString(Bytes, i, 1);
                }
                userDataLenghth = (userData.Length / 2).ToString("X2");
            }
        }


        /// <summary>
        /// 奇偶互换 (+F)
        /// </summary>
        /// <param name="str">要被转换的字符串</param>
        /// <returns>转换后的结果字符串</returns>
        private string ParityChange(string str)
        {
            string result = string.Empty;

            if (str.Length % 2 != 0)         //奇字符串 补F
            {
                str += "F";
            }
            for (int i = 0; i < str.Length; i += 2)
            {
                result += str[i + 1];
                result += str[i];
            }

            return result;
        }

        /// <summary>
        /// PDU编码器，完成PDU编码(USC2编码，最多70个字)
        /// </summary>
        /// <param name="phone">目的手机号码</param>
        /// <param name="Text">短信内容</param>
        /// <returns>编码后的PDU字符串</returns>
        public string PDUEncoder(string phone, string Text)
        {
            if (Text.Length > 70)
            {
                throw (new Exception("短信字数超过70"));
            }
            DestinationAddress = phone;
            UserData = Text;

            return serviceCenterAddress + protocolDataUnitType
                + messageReference + destinationAddress + protocolIdentifer
                + dataCodingScheme + validityPeriod + userDataLenghth + userData;
        }

        /// <summary>
        /// 7bit 编码
        /// </summary>
        /// <param name="phone">手机号码</param>
        /// <param name="Text">短信内容</param>
        /// <returns>编码后的字符串</returns>
        public string PDU7BitEncoder(string phone, string Text)
        {
            if (Text.Length > 160)
            {
                throw new Exception("短信字数大于160");
            }
            dataCodingScheme = "00";
            DestinationAddress = phone;
            UserData = Text;

            return serviceCenterAddress + protocolDataUnitType
                + messageReference + destinationAddress + protocolIdentifer
                + dataCodingScheme + validityPeriod + userDataLenghth + userData;
        }

        /// <summary>
        /// 重载 解码，返回信息字符串
        /// </summary>
        /// <param name="strPDU">短信PDU字符串</param>
        /// <returns>信息字符串（中心号码，手机号码，发送时间，短信内容）</returns>
        public string PDUDecoder(string strPDU)
        {
            int length = (Convert.ToInt32(strPDU.Substring(0, 2), 0x10) * 2) + 2;
            this.serviceCenterAddress = strPDU.Substring(0, length);
            int num2 = Convert.ToInt32(strPDU.Substring(length + 2, 2), 0x10);
            if ((num2 % 2) == 1)
            {
                num2++;
            }
            num2 += 4;
            this.originatorAddress = strPDU.Substring(length + 2, num2);
            this.dataCodingScheme = strPDU.Substring((length + num2) + 4, 2);
            this.serviceCenterTimeStamp = strPDU.Substring((length + num2) + 6, 14);
            this.userDataLenghth = strPDU.Substring((length + num2) + 20, 2);
            Convert.ToInt32(this.userDataLenghth, 0x10);
            this.userData = strPDU.Substring((length + num2) + 0x16);
            return (this.ServiceCenterAddress + "," + this.OriginatorAddress + "," + this.ServiceCenterTimeStamp + "," + this.UserData);
        }


        /// <summary>
        /// 完成手机或短信猫收到PDU格式短信的解码 暂时仅支持中文编码
        /// 未用DCS部分
        /// </summary>
        /// <param name="strPDU">短信PDU字符串</param>
        /// <param name="msgCenter">短消息服务中心 输出</param>
        /// <param name="phone">发送方手机号码 输出</param>
        /// <param name="msg">短信内容 输出</param>
        /// <param name="time">时间字符串 输出</param>
        public void PDUDecoder(string strPDU, out string msgCenter, out string phone, out string msg, out string time)
        {
            int lenSCA = Convert.ToInt32(strPDU.Substring(0, 2), 16) * 2 + 2;       //短消息中心占长度
            serviceCenterAddress = strPDU.Substring(0, lenSCA);

            int lenOA = Convert.ToInt32(strPDU.Substring(lenSCA + 2, 2), 16);           //OA占用长度
            if (lenOA % 2 == 1)                                                     //奇数则加1 F位
            {
                lenOA++;
            }
            lenOA += 4;                 //加号码编码的头部长度
            originatorAddress = strPDU.Substring(lenSCA + 2, lenOA);

            dataCodingScheme = strPDU.Substring(lenSCA + lenOA + 4, 2);             //DCS赋值，区分解码7bit

            serviceCenterTimeStamp = strPDU.Substring(lenSCA + lenOA + 6, 14);

            userDataLenghth = strPDU.Substring(lenSCA + lenOA + 20, 2);
            int lenUD = Convert.ToInt32(userDataLenghth, 16) * 2;
            userData = strPDU.Substring(lenSCA + lenOA + 22);

            msgCenter = ServiceCenterAddress;
            phone = OriginatorAddress;
            msg = UserData;
            time = ServiceCenterTimeStamp;
        }

        /// <summary>
        /// PDU7bit的解码，供UserData的get访问器调用
        /// </summary>
        /// <param name="len">用户数据长度</param>
        /// <param name="userData">数据部分PDU字符串</param>
        /// <returns></returns>
        private string PDU7bitDecoder(string userData)
        {
            string result = string.Empty;
            byte[] b = new byte[100];
            string temp = string.Empty;

            for (int i = 0; i < userData.Length; i += 2)
            {
                b[i / 2] = (byte)Convert.ToByte((userData[i].ToString() + userData[i + 1].ToString()), 16);
            }

            int j = 0;            //while计数
            int tmp = 1;            //temp中二进制字符字符个数
            while (j < userData.Length / 2 - 1)
            {
                string s = string.Empty;

                s = Convert.ToString(b[j], 2);

                while (s.Length < 8)            //s补满8位 byte转化来的 有的不足8位，直接解码将导致错误
                {
                    s = "0" + s;
                }

                result += (char)Convert.ToInt32(s.Substring(tmp) + temp, 2);        //加入一个字符 结果集 temp 上一位组剩余

                temp = s.Substring(0, tmp);             //前一位组多的部分

                if (tmp > 6)                            //多余的部分满7位，加入一个字符
                {
                    result += (char)Convert.ToInt32(temp, 2);
                    temp = string.Empty;
                    tmp = 0;
                }

                tmp++;
                j++;

                if (j == userData.Length / 2 - 1)           //最后一个字符
                {
                    result += (char)Convert.ToInt32(Convert.ToString(b[j], 2) + temp, 2);
                }
            }
            return result;
        }
    }
}
