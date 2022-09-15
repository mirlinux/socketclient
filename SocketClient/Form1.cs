using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;  // 추가
using System.Net; // 추가
using System.Net.Sockets;  // 추가
using System.IO;  // 추가

namespace SocketClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        StreamReader streamReader;
        StreamWriter streamWriter;

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(connect);
            thread1.IsBackground = true;
            thread1.Start();
        }

        private void connect() 
        {
            try
            {
                TcpClient tcpClient1 = new TcpClient();
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(textBox1.Text), int.Parse(textBox2.Text));
                tcpClient1.Connect(ipEnd);
                WriteRichTextbox("서버 연결됨...");

                streamReader = new StreamReader(tcpClient1.GetStream());
                streamWriter = new StreamWriter(tcpClient1.GetStream());
                streamWriter.AutoFlush = true;

                while (tcpClient1.Connected)
                {
                    string receiveData1 = streamReader.ReadLine();
                    WriteRichTextbox(receiveData1);
                }
            } catch (SocketException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void WriteRichTextbox(string data)
        {
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.AppendText(data + "\r\n"); });
            richTextBox1.Invoke((MethodInvoker)delegate { richTextBox1.ScrollToCaret(); });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sendData1 = textBox3.Text;
            streamWriter.WriteLine(sendData1);
        }
    }
}