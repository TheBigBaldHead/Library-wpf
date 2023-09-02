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
    public partial class SearchUser : Form
    {
        int mode = 0;
        string text = string.Empty;
        Form form;
        public SearchUser(Form form)
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            label4.Text = $"Search Between {Member.Members.Count} Members";
            listBox1.Visible = false;
            textBox1.Visible = false;
            this.form = form;
        }

        private void update()
        {
            text = textBox1.Text;
            int found = 0;
            foreach (var user in Member.Members)
            {
                bool valid = false;
                if (mode == 1) // NAME
                {
                    if (user.Name == textBox1.Text)
                    {
                        valid = true;
                    }
                }
                else if (mode == 2) // ID
                {
                    if (int.TryParse(textBox1.Text, out int ID))
                    {
                        if (user.ID == ID)
                        {
                            valid = true;
                        }
                    }

                }

                if (valid)
                {
                    found++;
                    listBox1.Items.Add($"{found}. ID: {user.ID}, Username: {user.Username}, Password: {user.Password}, Name: {user.Name}");
                }
            }
            if (found == 0)
            {
                listBox1.Items.Clear();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "NAME:";
            mode = 1;
            label2.Visible = true;
            label3.Visible = true;
            listBox1.Visible = true;
            textBox1.Visible = true;
            update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "USER ID:";
            mode = 2;
            label2.Visible = true;
            label3.Visible = true;
            listBox1.Visible = true;
            textBox1.Visible = true;
            update();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == text)
            {
                return;
            }
            update();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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
        private void SearchUser_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }

        private void SearchUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void SearchUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }
    }
}
