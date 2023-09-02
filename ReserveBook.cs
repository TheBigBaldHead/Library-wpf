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
    public partial class ReserveBook : Form
    {
        Form form;
        Member member;
        public ReserveBook(Form form, Member member)
        {
            InitializeComponent();
            this.form = form;
            this.member = member;
        }

        private void ReserveBook_Load(object sender, EventArgs e)
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
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Fill the box!");
                return;
            }
            if (int.TryParse(textBox1.Text, out int ID))
            {
                Book book = Book.Search(ID);
                if (book != null)
                {
                    foreach (var id in member.Book_ids_Reserve)
                    {
                        if (ID == id)
                        {
                            MessageBox.Show($"You have already reserved this book!");
                            return;
                        }
                    }
                    foreach (var id in member.Book_ids_Borrow)
                    {
                        if (ID == id)
                        {
                            MessageBox.Show($"You have already borrowed this book!");
                            return;
                        }
                    }
                    if (book.Mem_Ids_Reserve.Count == 0)
                    {
                        MessageBox.Show($"You can borrow the book now!");
                        return;
                    }
                    book.Reserve(member);
                    MessageBox.Show($"Book with ID {ID} reserved successfully!");
                    form.Location = Location;
                    form.Visible = true;
                    Close();
                    return;
                }
                else
                {
                    MessageBox.Show("Book not found!");
                }
            }
            else
            {
                MessageBox.Show("Invalid ID!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form form = new SearchBook(this);
            form.Show();
            Visible = false;
        }

        private void ReserveBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void ReserveBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
