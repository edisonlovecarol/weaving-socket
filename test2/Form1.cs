﻿using client;
using MyInterface;
using StandardModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace test2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        P2Pclient p2pc = new P2Pclient(false);
        private void Form1_Load(object sender, EventArgs e)
        {
            p2pc.receiveServerEvent += P2pc_receiveServerEvent;//接收数据事件
            p2pc.timeoutevent += P2pc_timeoutevent;//超时（掉线）事件
            p2pc.start("127.0.0.1", 11002, true);//11002 是网关的端口号，刚才WEB网关占用了11001，我改成11002了
            p2pc.AddListenClass(this);//这是表示  [InstallFun("forever")]的方法，在哪个类中
        }
        [InstallFun("forever")]//客户端也支持像服务端那样写，刚才看懂返回的内容也是testaabb，所以客户端也要把方法命名testaabb
        public void testaabb(System.Net.Sockets.Socket soc, _baseModel _0x01)
        {
            MessageBox.Show(_0x01.Getjson());
            //  Gw_EventMylog("",_0x01.Getjson());
        }
        private void P2pc_timeoutevent()
        {
            if (!p2pc.Isline)
            {
                p2pc.Restart(true);
            }
        }

        private void P2pc_receiveServerEvent(byte command, string text)
        {
           
        }
        public class A
        {
            String str = "";

            public string Str
            {
                get
                {
                    return str;
                }

                set
                {
                    str = value;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //在加个发送
            A a = new A();
            a.Str = "ccccccc";
            p2pc.SendRoot<A>(0x23, "testaabb", a,0);
            //这样就可以了，我们试试
        }
    }
}
