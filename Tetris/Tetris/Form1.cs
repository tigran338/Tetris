using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Tetris
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Shape a;
        static Random rn = new Random();
        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            button2.Visible = false;
            List<int> ls = new List<int>();
            try
            {
                StreamReader sr = new StreamReader("input.txt");
                while (!sr.EndOfStream)
                {
                    ls.Add(int.Parse(sr.ReadLine()));
                }
                sr.Close();
                label5.Text = label5.Text.Split()[0] + " " + ls.Max();
            }
            catch { }
        }

        public void _Time(object sender, EventArgs e)
        {
            if (!a.Move())
            {
                a = new Shape(panel1,label1,label2,button2);
                a.Creat(rn.Next(7));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode.ToString())
                {
                    case "A": if (TimerT.Enabled)a.Control(1); break;
                    case "D": if (TimerT.Enabled) a.Control(2); break;
                    case "S": if (TimerT.Enabled) a.Control(3); break;
                    case "P": TimerT.Enabled = !TimerT.Enabled; break;
                    case"Space":a.Move();break;   
                    default: break;
                }
            }
            catch { }
        }
        Timer TimerT;
        private void button1_Click(object sender, EventArgs e)
        {
            label3.Visible = false;
            label4.Visible = false;
            button1.Visible = false;
            textBox1.Visible = false;
            label1.Visible = true;
            a = new Shape(panel1,label1,label2,button2);
            a.Creat(rn.Next(7));
            TimerT = new Timer();
            TimerT.Enabled = true;
            try
            {
                TimerT.Interval = int.Parse(textBox1.Text);
            }
            catch { TimerT.Interval = 400; }
            TimerT.Tick += new System.EventHandler(_Time);
            TimerT.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
        
    }
}
