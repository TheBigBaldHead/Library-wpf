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
    public partial class User : Form
    {
        Form form;
        Member member;
        public User(Form form, Member member)
        {
            InitializeComponent();
            this.form = form;
            this.member = member;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new SearchBook(this);
            form.Show();
            Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new ReservedBooks(this, member);
            form.Show();
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new BorrowBook(this, member);
            form.Show();
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new ReserveBook(this, member);
            form.Show();
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new ReturnBook(this, member);
            form.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new ModifyUser(this, member);
            form.Show();
            Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
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
        private void User_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }

        private void User_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void User_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
