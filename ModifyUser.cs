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
    public partial class ModifyUser : Form
    {
        Form form;
        Member member;
        public ModifyUser(Form form, Member member)
        {
            InitializeComponent();
            this.form = form;
            this.member = member;
            textBox3.Text = member.Name;
            textBox2.Text = member.Username;
            textBox1.Text = member.Password;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Member.Find(textBox2.Text) == null)
            {
                Member MemberUpdate = member;
                MemberUpdate.Name = textBox3.Text;
                MemberUpdate.Username = textBox2.Text;
                MemberUpdate.Password = textBox1.Text;
                member.Modify(MemberUpdate);
                MessageBox.Show("Profile updated successfully!");
                form.Location = Location;
                form.Visible = true;
                Close();
            }
            else
            {
                if (textBox3.Text == member.Name && textBox2.Text == member.Username && textBox1.Text == member.Password)
                {
                    MessageBox.Show("No change detected!");
                    return;
                }
                MessageBox.Show("This username already exists!");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void ModifyUser_Load(object sender, EventArgs e)
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
        private void ModifyUser_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void ModifyUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }
    }
}
