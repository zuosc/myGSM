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
using System.Linq;

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
            set { try { this.sp.PortName = value; } catch { } }
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
            string temp = string.Empty;
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
                        temp = temp.Split(Environment.NewLine.ToCharArray()).OrderByDescending(it => it.Length).FirstOrDefault();
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
            return digestAT_CMGL(temp);
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
            return digestAT_CMGL(temp);
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
            return digestAT_CMGL(temp);
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
            return digestAT_CMGL(temp);
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllMsg()
        {
            string[] temp = null;
            string tt = string.Empty;

            tt = this.SendAT("AT+CMGL=4");//读取所有信息
            if (tt.Substring(tt.Length - 4, 3).Trim() == "OK")
            {
                temp = tt.Split(Environment.NewLine.ToCharArray());
            }
            return digestAT_CMGL(temp);
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
            catch { this.sp.DataReceived += this.sp_DataReceived; }
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
                if (phone.StartsWith("+"))
                {
                    phone = phone.Remove(0, 1).Replace(" ", "");
                }
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
            /*  if (phone.Substring(0, 2) != "86")
              {
                  phone = string.Format("86{0}", phone);
              }*/

            if (phone.StartsWith("+"))
            {
                phone = phone.Remove(0, 1).Replace(" ", "");
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
            catch { return false; }
            return true;
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

            // temp = temp.Split((char)(13))[3];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串
            temp = temp.Split(Environment.NewLine.ToCharArray()).OrderByDescending(it => it.Length).FirstOrDefault();

            pe.PDUDecoder(temp, out msgCenter, out phone, out msg, out time);

            if (AutoDelMsg)//如果阅读完短信自动删除设置为真
            {
                try
                {
                    DelMsgByIndex(index);
                }
                catch { }
            }
            return util.formatMsg(index.ToString(), phone, msg, time);
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

            temp = temp.Split((char)(13))[2];       //取出PDU串(char)(13)为0x0a即\r 按\r分为多个字符串 第3个是PDU串

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

        /// <summary>
        /// 解析AT+CMGL=4 获取短信命令CMGL相关的响应
        /// </summary>
        /// <param name="list">响应按换行分割得到的数组</param>
        /// <returns></returns>
        private List<string> digestAT_CMGL(string[] list)
        {
            PDUEncoding pe = new PDUEncoding();
            List<string> myResult = new List<string>();
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].StartsWith("+CMGL:"))
                {
                    String index = list[i].Split(',')[0].Last().ToString();
                    myResult.Add(pe.PDUDecoder(index, list[i + 1]));
                }
            }
            return myResult;
        }
    }
}
