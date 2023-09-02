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
    public partial class AddBook : Form
    {
        Form form;
        public AddBook(Form form)
        {
            InitializeComponent();
            foreach (var item in Enum.GetNames(typeof(Subject)))
            {
                listBox1.Items.Add(item);
            }
            this.form = form;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty)
            {
                MessageBox.Show("Fill the boxes!");
                return;
            }
            if (int.TryParse(textBox2.Text, out int Date))
            {
                if (listBox1.SelectedItem == null)
                {
                    MessageBox.Show("Choose a subject!");
                    return;
                }
                if (Enum.TryParse(listBox1.SelectedItem.ToString(), out Subject subject))
                {
                    for (int i = 0; i < Book.Books.Count; i++)
                    {
                        if (Book.Books[i].Name == textBox1.Text && Book.Books[i].Subject == subject && Book.Books[i].Date == Date)
                        {
                            Book.Books[i].Count++;
                            MessageBox.Show($"Another book added successfully to the ID {Book.Books[i].ID}!");
                            form.Location = Location;
                            form.Visible = true;
                            Close();
                            return;
                        }
                    }

                    Book book = new Book(textBox1.Text, Book.Books.Count + 1, subject, Date, 1, new List<int>());
                    book.BorrowedID = 0;
                    book.Register();
                    MessageBox.Show($"Book added successfully with ID {book.ID}!");
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
            }
            
        }
        private void set_background(Object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Rectangle gradient_rectangle = new Rectangle(0, 0, Width, Height);
            Brush b = new LinearGradientBrush(gradient_rectangle, Color.FromArgb(255, 255, 255), Color.FromArgb(57, 128, 227), 65f);
            graphics.FillRectangle(b, gradient_rectangle);
        }
        private void AddBook_Load(object sender, EventArgs e)
        {
            Paint += new PaintEventHandler(set_background); 
            FormBorderStyle = FormBorderStyle.FixedSingle;
            StartPosition = FormStartPosition.Manual;
            Location = new Point(form.Location.X, form.Location.Y);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.Location = Location;
            form.Visible = true;
            Close();
        }

        private void AddBook_FormClosed(object sender, FormClosedEventArgs e)
        {
            FileUpdate.UpdateFile();
            if (form.Visible == false)
            {
                form.Close();
            }
        }

        private void AddBook_FormClosing(object sender, FormClosingEventArgs e)
        {
            while (Opacity > 0)
            {
                Opacity -= 0.0001;
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
