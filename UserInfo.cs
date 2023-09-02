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
    public partial class UserInfo : Form
    {
        Form form;
        public UserInfo(Form form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int ID))
            {
                foreach (var user in Member.Members)
                {
                    if (user.ID == ID)
                    {
                        label7.Text = user.Name;
                        label8.Text = user.Username;
                        label9.Text = user.Password;
                        if (user.Book_ids_Borrow.Count > 0)
                        {
                            label12.Text = "BORROWED BOOK IDS:";
                            label13.Text = string.Join(", ", user.Book_ids_Borrow);
                        }
                        else
                        {
                            label12.Text = "NO BOOKS BORROWED";
                        }
                        foreach (var id in user.Book_ids_Reserve)
                        {
                            listBox1.Items.Add(id.ToString());
                        }
                        return;
                    }
                }
            }
            label7.Text = string.Empty;
            label8.Text = string.Empty;
            label9.Text = string.Empty;
            label12.Text = string.Empty;
            label13.Text = string.Empty;
            listBox1.Items.Clear();
        }
        private void set_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(57, 128, 227), 65f);
            graphics.FillRectangle(b, gradient_rectangle);
        }
        private void UserInfo_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void UserInfo_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void UserInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form form = new SearchUser(this);
            form.Show();
            Visible = false;
        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
    }
}
