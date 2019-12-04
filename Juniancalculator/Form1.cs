using System;
using System.Collections;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Juniancalculator
{
    public partial class Form1 : Form
    {
        ArrayList szarr = new ArrayList();
        ArrayList fharr = new ArrayList();
        bool isjia = false;
        int jilu;
        public class Denyuhou
        {
            public string fuhao;
            public float shuzi;
            public bool isdenyuhou;
        }
        Denyuhou xujia = new Denyuhou();

        public Form1()
        {
            this.StartPosition = FormStartPosition.CenterScreen; //设置屏幕居中打开程序
            InitializeComponent();
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

        public static string Qiuhe(ArrayList szarr, ArrayList fharr)
        {
            if (szarr.Count == 0)
            {
                return "0";
            }
            else if (szarr.Count == 1)
            {
                return szarr[0].ToString();
            }
            else
            {
                float jieguo = (float)szarr[0];
                for (int i = 1; i < szarr.Count; i++)
                {
                    switch (fharr[i - 1].ToString())
                    {
                        case "+":
                            jieguo += (float)szarr[i];
                            break;
                        case "-":
                            jieguo -= (float)szarr[i];
                            break;
                        case "/":
                            jieguo = jieguo / (float)szarr[i];
                            break;
                        case "*":
                            jieguo = jieguo * (float)szarr[i];
                            break;
                    }
                }
                return jieguo.ToString();
            }

        }

        private void jiajian_Click(object sender, EventArgs e)
        {
            if (label1.Text != "" && !isjia)
            {
                isjia = true;
                xujia.isdenyuhou = false;
                Button bs = (Button)sender;
                if (bs.Text == "+")
                {
                    szarr.Add(float.Parse(label1.Text));
                    fharr.Add("+");
                    label2.Text += label1.Text + "+";
                    label1.Text = Qiuhe(szarr, fharr);
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
            if (bs.Text == "%" && label1.Text != "")
            {
                string sss = Math.Round((float.Parse(label1.Text) * 0.01), 10).ToString();
                label1.Text = sss == "0" ? "" : sss;
            }
            else if ((label1.Text == "" || Regex.IsMatch(label1.Text, "E")) && bs.Text == ".")
            {
                label1.Text = "0.";
            }
            else if (label1.Text == "0" && Regex.IsMatch(bs.Text, @"[1-9]"))
            {
                label1.Text = bs.Text;
            }
            else if (label1.Text.Length < 13 && Regex.IsMatch(bs.Text, @"[1-9]"))
            {
                label1.Text += bs.Text;
            }
            else if (label1.Text == "0" && bs.Text == "0")
            {

            }
            else if (bs.Text == "." && Regex.IsMatch(label1.Text, "."))
            {

            }
        }

        private void teshu_Click(object sender, EventArgs e)
        {
            if (label1.Text != "0" && label1.Text != "")
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
                    label1.Text = label1.Text.Length < 13 ? Math.Pow(float.Parse(label1.Text), 2).ToString() : "";
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
            xujia.isdenyuhou = false;
            szarr.Clear();
            fharr.Clear();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label1.Text != "" && szarr.Count == fharr.Count)
            {
                ArrayList al = new ArrayList();
                al.Add(jilu);
                if (xujia.isdenyuhou)
                {
                    al.Add(label1.Text + xujia.fuhao + xujia.shuzi + "=");
                    szarr.Add(float.Parse(label1.Text));
                    szarr.Add(xujia.shuzi);
                    fharr.Add(xujia.fuhao);
                    label1.Text = Qiuhe(szarr, fharr);
                    al.Add(label1.Text);
                    add_jilu(al);
                    szarr.Clear();
                    fharr.Clear();
                    jilu += 1;
                }
                else
                {
                    al.Add(label2.Text + label1.Text + "=");
                    szarr.Add(float.Parse(label1.Text));
                    xujia.shuzi = float.Parse(label1.Text);
                    label1.Text = Qiuhe(szarr, fharr);
                    al.Add(label1.Text);
                    if (fharr.Count - 1 >= 0)
                    {
                        xujia.fuhao = fharr[fharr.Count - 1].ToString();
                        xujia.isdenyuhou = true;
                    }
                    add_jilu(al);
                    label2.Text = "";
                    szarr.Clear();
                    fharr.Clear();
                    jilu += 1;
                    isjia = true;
                }
            }
        }

        private void add_jilu(ArrayList xsdwe)
        {
            int i = (int)xsdwe[0];
            Label lb1 = new Label();
            Label lb2 = new Label();
            Label lb3 = new Label();
            Button lb4 = new Button();
            lb1.Name = "lb1_" + i;
            lb2.Name = "lb2_" + i;
            lb3.Name = "lb3_" + i;
            lb4.Name = "lb4_" + i;
            lb4.BackColor = Color.Transparent;
            lb4.FlatAppearance.BorderSize = 0;
            lb4.FlatStyle = FlatStyle.Flat;
            lb4.Font = new Font("微软雅黑", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lb4.ForeColor = Color.Black;
            lb4.Location = new Point(18, i * 72 + 18);
            lb4.Size = new Size(36, 36);
            lb4.Text = "×";
            lb4.Click += new EventHandler(shanchu_Click);
            this.panel5.Controls.Add(lb4);
            lb1.Text = xsdwe[1].ToString();
            lb1.Size = new Size(460, 20);
            lb1.Font = new Font("微软雅黑", 9);
            lb1.Location = new Point(5, i * 72 + 10);
            this.panel5.Controls.Add(lb1);
            lb2.Text = xsdwe[2].ToString();
            lb2.Size = new Size(470, 28);
            lb2.Font = new Font("微软雅黑", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lb2.Location = new Point(5, i * 72 + 32);
            this.panel5.Controls.Add(lb2);
            lb3.BorderStyle = BorderStyle.Fixed3D;
            lb3.Size = new Size(480, 2);
            lb3.Location = new Point(5, i * 72 + 72);
            this.panel5.Controls.Add(lb3);
            lb1.TextAlign = ContentAlignment.MiddleRight;
            lb2.TextAlign = ContentAlignment.MiddleRight;
            lb1.BackColor = Color.Transparent;
            lb2.BackColor = Color.Transparent;
            lb3.BackColor = Color.Transparent;
            panel4.VerticalScroll.Value = panel4.VerticalScroll.Maximum;
        }

        private void shanchu_Click(object sender, EventArgs e)
        {
            Button b1 = (Button)sender;
            int i = int.Parse(Regex.Matches(b1.Name, @"(?<=_)\d+$")[0].Value);
            foreach (Control cont in panel5.Controls)
            {
                int j = int.Parse(Regex.Matches(cont.Name, @"(?<=_)\d+$")[0].Value);
                if (j == i)
                {
                    panel5.Controls.Remove(cont);
                }
                else if (j >= i)
                {
                    cont.Name = cont.Name.Substring(0, cont.Name.Length - j.ToString().Length) + (j - 1).ToString();
                    cont.Location = new Point(cont.Location.X, cont.Location.Y - 72);
                }
            }
            jilu -= 1;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            string anxia = e.KeyChar.ToString();
            if (Regex.IsMatch(anxia, @"[0-9]|\.|%"))
            {

                if (isjia)
                {
                    label1.Text = "";
                    isjia = false;
                }
                if (anxia == "%" && label1.Text != "")
                {
                    string sss = Math.Round((float.Parse(label1.Text) * 0.01), 10).ToString();
                    label1.Text = sss == "0" ? "" : sss;
                }
                else if (anxia == "." && Regex.IsMatch(label1.Text, ".")) { }
                else if ((label1.Text == "" || Regex.IsMatch(label1.Text, "E")) && anxia == ".")
                {
                    label1.Text = "0.";
                }
                else if (label1.Text == "0" && Regex.IsMatch(anxia, @"[1-9]"))
                {
                    label1.Text = anxia;
                }
                else if (label1.Text.Length < 13 && Regex.IsMatch(anxia, @"[1-9]"))
                {
                    label1.Text += anxia;
                }
                else if (label1.Text == "0" && anxia == "0") { }
            }
            else if (Regex.IsMatch(anxia, @"\+|\-|\*|\/|\^"))
            {
                if (label1.Text != "" && !isjia)
                {
                    isjia = true;
                    if (anxia == "+")
                    {
                        szarr.Add(float.Parse(label1.Text));
                        fharr.Add("+");
                        label2.Text += label1.Text + "+";
                        label1.Text = Qiuhe(szarr, fharr);
                    }
                    else if (anxia == "-")
                    {
                        szarr.Add(float.Parse(label1.Text));
                        fharr.Add("-");
                        label2.Text += label1.Text + "－";
                        label1.Text = Qiuhe(szarr, fharr);
                    }
                    else if (anxia == "*")
                    {
                        szarr.Add(float.Parse(label1.Text));
                        fharr.Add("*");
                        label2.Text += label1.Text + "×";
                        label1.Text = Qiuhe(szarr, fharr);
                    }
                    else if (anxia == "/")
                    {
                        szarr.Add(float.Parse(label1.Text));
                        fharr.Add("/");
                        label2.Text += label1.Text + "÷";
                        label1.Text = Qiuhe(szarr, fharr);
                    }
                    else if (anxia == "^")
                    {
                        label1.Text = label1.Text.Length < 13 ? Math.Pow(float.Parse(label1.Text), 2).ToString() : "";
                    }
                }

            }
        }

    }
}