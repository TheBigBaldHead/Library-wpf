using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        string username;
        string password;
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.CenterScreen;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            username = textBox1.Text;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (username == "admin" && password == "admin")
            {
                Form admin = new Admin(this);
                admin.Show();
                Visible = false;
                return;
            }
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Fill the boxes!");
                return;
            }
            for (int i = 0; i < Member.Members.Count; i++)
            {
                if (Member.Members[i].Username == username)
                {
                    if (Member.Members[i].Password == password)
                    {
                        Form user = new User(this, Member.Members[i]);
                        user.Show();
                        Visible = false;
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Wrong password!");
                        return;
                    }
                }
            }
            MessageBox.Show("Username not found!");
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form form = new Form2(this);
            form.Show();
            Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FileUpdate.ReadFile(ref Book.Books, ref Member.Members);
        }

        private void set_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(57, 128, 227), 65f);       
            graphics.FillRectangle(b, gradient_rectangle);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
        }
    }
}
