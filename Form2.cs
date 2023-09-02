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
    public partial class Form2 : Form
    {
        string username = string.Empty;
        string password = string.Empty;
        string name = string.Empty;
        Form form;
        public Form2(Form form)
        {
            Paint += new PaintEventHandler(set_background);
            this.form = form;
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            username = textBox1.Text;
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            password = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            name = textBox3.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (username == string.Empty || password == string.Empty || name == string.Empty)
            {
                MessageBox.Show("Fill the boxes!");
                return;
            }
            foreach (var membercheck in Member.Members)
            {
                if (membercheck.Username == username)
                {
                    MessageBox.Show("This username already exists!");
                    return;
                }
            }
            Member member = new Member(username, password, name, Member.Members.Count + 1, 0, new List<int>(), new List<int>());
            member.Register();
            MessageBox.Show("User added successfully!");
            form.Location = Location;
            form.Visible = true;
            Close();
        }
        private void set_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(57, 128, 227), 65f);
            graphics.FillRectangle(b, gradient_rectangle);
        }
        private void Form2_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {

            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }
    }
}
