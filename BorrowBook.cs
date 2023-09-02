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
    public partial class BorrowBook : Form
    {
        public Form form;
        public Member member;
        public BorrowBook(Form form, Member member)
        {
            InitializeComponent();
            this.form = form;
            this.member = member;
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
                if (book != null) // Book exists
                {
                    if (book.BorrowedID != 0)
                    {
                        if (book.BorrowedID == member.ID)
                        {
                            MessageBox.Show("You have already borrowed this book!");
                            return;
                        }
                        MessageBox.Show("Someone else has borrowed this book!");
                        return;
                    }
                    foreach (var id in member.Book_ids_Borrow)
                    {
                        if (id == ID)
                        {
                            MessageBox.Show("You have already borrowed this book!");
                            return;
                        }
                    }
                    if (book.Mem_Ids_Reserve.Count > 0) // If book has been reserved
                    {
                        if (book.Mem_Ids_Reserve[0] != member.ID) // If it's not his turn to borrow the book
                        {
                            MessageBox.Show("Someone else has reserved this book before you!");
                            return;
                        }
                        book.Mem_Ids_Reserve.RemoveAt(0); // It was this member's turn to borrow the book
                    }
                    if (member.Borrow(ref book)) // If member has borrowed less than 5 books
                    {
                        MessageBox.Show($"Book with ID {ID} borrowed successfully!");
                        FileUpdate.UpdateFile();
                        form.Location = Location;
                        form.Visible = true;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show($"You have reached your limit for borrowing books!");
                    }
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

        private void button1_Click(object sender, EventArgs e)
        {
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void BorrowBook_Load(object sender, EventArgs e)
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
        private void BorrowBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void BorrowBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void button2_FontChanged(object sender, EventArgs e)
        {

        }
    }
}
