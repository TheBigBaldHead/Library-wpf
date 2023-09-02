using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Drawing.Drawing2D;

namespace WindowsFormsApp1
{
    public partial class ModifyBook : Form
    {
        Form form;
        public ModifyBook(Form form)
        {
            InitializeComponent();
            Size = new Size(form.Width, form.Height);
            foreach (var item in Enum.GetNames(typeof(Subject)))
            {
                listBox1.Items.Add(item);
            }
            textBox3.Enabled = false;
            textBox2.Enabled = false;
            listBox1.Enabled = false;
            this.form = form;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int ID))
            {
                foreach (var book in Book.Books)
                {
                    if (book.ID == ID) 
                    {
                        textBox3.Enabled = true;
                        textBox2.Enabled = true;
                        listBox1.Enabled = true;
                        return;
                    }
                }
            }
            textBox3.Enabled = false;
            textBox2.Enabled = false;
            listBox1.Enabled = false;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty)
            {
                if (int.TryParse(textBox1.Text, out int ID))
                {
                    if (textBox3.Text == string.Empty || textBox2.Text == string.Empty)
                    {
                        MessageBox.Show("Fill the boxes.");
                        return;
                    }
                    if (int.TryParse(textBox2.Text, out int Date))
                    {
                        if (Enum.TryParse(listBox1.SelectedItem.ToString(), out Subject subject))
                        {
                            Book book = Book.Search(ID);
                            book.Name = textBox3.Text;
                            book.Date = Date;
                            book.Subject = subject;
                            book.Modify(book);
                            MessageBox.Show("Book modified successfully!");
                            form.Location = Location;
                            form.Visible = true;
                            Close();
                        }
                        else
                        {
                            MessageBox.Show("Choose a valid subject!");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid date!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Book ID!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Fill the boxes.");
            }
        }

        private void ModifyBook_Load(object sender, EventArgs e)
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
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void ModifyBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void ModifyBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }
    }
}
