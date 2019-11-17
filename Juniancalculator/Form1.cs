using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Juniancalculator
{
    public partial class Form1 : Form
    {
        ArrayList szarr = new ArrayList();
        ArrayList fharr = new ArrayList();
        bool isjia = false;
        int jilu;
        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen; //设置屏幕居中打开程序
            InitializeComponent();
        }
        public static string Qiuhe(ArrayList szarr, ArrayList fharr)
        {
            if (szarr.Count == 0)
            {
                return "0";
            }
            else if(szarr.Count == 1)
            {
                return szarr[0].ToString();
            }
            else
            {
                float jieguo = (float)szarr[0];
                for (int i = 1; i < szarr.Count; i++)
                {
                    switch (fharr[i-1].ToString())
                    {
                        case "+":
                            jieguo += (float)szarr[i];
                            break;
                        case "-":
                            jieguo -= (float)szarr[i];
                            break;
                        case "/":
                            jieguo =jieguo/ (float)szarr[i];
                            break;
                        case "*":
                            jieguo =jieguo* (float)szarr[i];
                            break;
                    }
                }
                return jieguo.ToString();
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control cont in panel1.Controls)
            {
                cont.Click += new EventHandler(shuzi_Click);
            }
            foreach (Control cont in panel3.Controls)
            {
                cont.Click += new EventHandler(teshu_Click);
            }
            foreach (Control cont in panel2.Controls)
            {
                cont.Click += new EventHandler(jiajian_Click);
            }
        }
        private void jiajian_Click(object sender, EventArgs e)
        {
            if (label1.Text != "" && !isjia)
            {
                isjia = true;
                Button bs = (Button)sender;
                if (bs.Text == "+")
                {
                    szarr.Add(float.Parse(label1.Text));
                    fharr.Add("+");
                    label2.Text += label1.Text + "+";
                    label1.Text = Qiuhe(szarr,fharr);
                }
                else if (bs.Text == "－")
                {
                    szarr.Add(float.Parse(label1.Text));
                    fharr.Add("-");
                    label2.Text += label1.Text + "－";
                    label1.Text = Qiuhe(szarr, fharr);
                }
                else if (bs.Text == "×")
                {
                    szarr.Add(float.Parse(label1.Text));
                    fharr.Add("*");
                    label2.Text += label1.Text + "×";
                    label1.Text = Qiuhe(szarr, fharr);
                }
                else if (bs.Text == "÷")
                {
                    szarr.Add(float.Parse(label1.Text));
                    fharr.Add("/");
                    label2.Text += label1.Text + "÷";
                    label1.Text = Qiuhe(szarr, fharr);
                }
            }
        }
        private void shuzi_Click(object sender, EventArgs e)
        {
            Button bs = (Button)sender;
            if (isjia)
            {
                label1.Text = "";
                isjia = false;
            }
            if (bs.Text == "%")
            {
                string sss=Math.Round((float.Parse(label1.Text) * 0.01),10).ToString();
                label1.Text = sss == "0" ? "" : sss;
            }else if((label1.Text==""|| Regex.IsMatch(label1.Text, "E")) && bs.Text == ".")
            {
                label1.Text = "0.";
            }
            else if (label1.Text == "0" && Regex.IsMatch(bs.Text, @"[1-9]"))
            {
                label1.Text = bs.Text;
            }
            else if(label1.Text.Length<13)
            {
                label1.Text += bs.Text;
            }
            else if (label1.Text == "0" && bs.Text == "0")
            {

            }
        }
        private void teshu_Click(object sender, EventArgs e)
        {   if(label1.Text != "0" && label1.Text != "")
            {
                Button bs = (Button)sender;
                if (bs.Text == "±")
                {
                    label1.Text = label1.Text.Substring(0, 1) == "-" ? label1.Text.Substring(1) : "-" + label1.Text;
                }
                else if (bs.Text == "²√")
                {
                    label1.Text = Math.Sqrt(float.Parse(label1.Text)).ToString();
                }
                else if (bs.Text == "x²")
                {
                    label1.Text = label1.Text.Length < 13?Math.Pow(float.Parse(label1.Text),2).ToString():"";
                }
            }           
        }
        private void button18_Click(object sender, EventArgs e)
        {
            if (label1.Text != "")
            {
                label1.Text = label1.Text.Substring(0, label1.Text.Length - 1);          
            }
        }
        private void button_C_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            szarr.Clear();
            fharr.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {   
            if(label1.Text != "" && szarr.Count==fharr.Count)
            {
                ArrayList al = new ArrayList();
                al.Add(jilu);
                al.Add(label2.Text + label1.Text + "=");
                szarr.Add(float.Parse(label1.Text));
                label1.Text = Qiuhe(szarr, fharr);
                al.Add(label1.Text);
                add_jilu(al);
                label2.Text="";
                szarr.Clear();
                fharr.Clear();
                jilu += 1;
            }
        }
        private void add_jilu(ArrayList xsdwe)
        {
            int i = (int)xsdwe[0];
            Label lb1 = new Label();
            Label lb2 = new Label();
            Label lb3 = new Label();
            lb1.Name = "lb1_" + i;
            lb2.Name = "lb2_" + i;
            lb3.Name = "lb3_" + i;
            lb1.Text = xsdwe[1].ToString();
            lb1.Size = new Size(480, 20);
            lb1.Font = new Font("微软雅黑", 9);
            lb1.Location = new Point(5, i * 72 + 10);
            this.panel4.Controls.Add(lb1);
            lb2.Text = xsdwe[2].ToString();
            lb2.Size = new Size(480, 28);
            lb2.Font = new Font("微软雅黑", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lb2.Location = new Point(5, i * 72 + 32);
            this.panel4.Controls.Add(lb2);
            lb3.BorderStyle = BorderStyle.Fixed3D;
            lb3.Size = new Size(480, 2);    
            lb3.Location = new Point(5, i * 72 + 72);
            this.panel4.Controls.Add(lb3);
            lb1.TextAlign = ContentAlignment.MiddleRight;
            lb2.TextAlign = ContentAlignment.MiddleRight;
            lb1.BackColor = Color.Transparent;
            lb2.BackColor = Color.Transparent;
            lb3.BackColor = Color.Transparent;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
