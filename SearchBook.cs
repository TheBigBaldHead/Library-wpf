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
    public partial class SearchBook : Form
    {
        Form form;
        int mode = 0;
        string text = string.Empty;
        public SearchBook(Form form)
        {
            InitializeComponent();
            label2.Visible = false;
            label3.Visible = false;
            label4.Text = $"Search Between {Book.Books.Count} Books!";
            listBox1.Visible = false;
            textBox1.Visible = false;
            this.form = form;
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

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

        private void label2_Click(object sender, EventArgs e)
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
        private void update()
        {
            listBox1.Items.Clear();
            text = textBox1.Text;
            int found = 0;
            foreach (var book in Book.Books)
            {
                bool valid = false;
                if (mode == 1) // NAME
                {
                    if (book.Name == textBox1.Text)
                    {
                        valid = true;
                    }
                }
                else if (mode == 2) // ID
                {
                    if (int.TryParse(textBox1.Text, out int ID))
                    {
                        if (book.ID == ID)
                        {
                            valid = true;
                        }
                    }

                }
                else if (mode == 3) // SUBJECT
                {
                    if (Enum.TryParse(textBox1.Text, true, out Subject subject))
                    {
                        if (!int.TryParse(textBox1.Text, out int ID)) 
                        { 
                            if (book.Subject == subject)
                            {
                                valid = true;
                            }
                        }
                    }
                }

                if (valid)
                {
                    found++;
                    listBox1.Items.Add($"{found}. {book.Display()}");
                }
            }
            if (found == 0)
            {
                listBox1.Items.Clear();
            }
        }
        private void set_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(57, 128, 227), 65f);
            graphics.FillRectangle(b, gradient_rectangle);
        }
        private void label3_Click(object sender, EventArgs e)
        {

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

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "BOOK ID:";
            mode = 2;
            label2.Visible = true;
            label3.Visible = true;
            listBox1.Visible = true;
            textBox1.Visible = true;
            update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Text = "SUBJECT:";
            mode = 3;
            label2.Visible = true;
            label3.Visible = true;
            listBox1.Visible = true;
            textBox1.Visible = true;
            update();
        }

        private void SearchBook_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }
        private void SearchBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void SearchBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }
    }
}
