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
    public partial class Admin : Form
    {
        Form form;
        public Admin(Form form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }
        private void set_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(57, 128, 227), 65f);
            graphics.FillRectangle(b, gradient_rectangle);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form modform = new ModifyBook(this);
            modform.Show();
            Visible = false;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new AddBook(this);
            form.Show();
            Visible = false;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form form = new UserInfo(this);
            form.Show();
            Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form form = new BookInfo(this);
            form.Show();
            Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new SearchBook(this);
            form.Show();
            Visible = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new SearchUser(this);
            form.Show();
            Visible = false;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form form = new DeleteUser(this);
            form.Show();
            Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form form = new DeleteBook(this);
            form.Show();
            Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
