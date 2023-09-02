using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WindowsFormsApp1
{
    public class FileUpdate
    {
        static public void UpdateFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("MemberData.txt"))
                {
                    foreach (var member in Member.Members)
                    {
                        writer.WriteLine($"{member.ID}`{member.Name}`{member.Username}`{member.Password}`{string.Join(",", member.Book_ids_Borrow.Select(x => x.ToString()))}`{string.Join(",", member.Book_ids_Reserve.Select(x => x.ToString()))}");
                    }
                }
                using (StreamWriter writer = new StreamWriter("BookData.txt"))
                {
                    foreach (var book in Book.Books)
                    {
                        writer.WriteLine($"{book.ID}`{book.Name}`{book.Date}`{book.Subject}`{book.Count}`{book.BorrowedID}`{string.Join(",", book.Mem_Ids_Reserve.Select(x => x.ToString()))}");
                    }
                }
            }
            catch { }
        }
        static public void ReadFile(ref List<Book> Books, ref List<Member> Members)
        {
            try
            {
                if (!File.Exists("MemberData.txt"))
                    File.Create("MemberData.txt");
                if (!File.Exists("BookData.txt"))
                    File.Create("BookData.txt");
                List<int> ReservedMemberIDs = new List<int>();
                List<int> BorrowedBookIDs = new List<int>();
                List<int> ReservedBookIDs = new List<int>();
                using (StreamReader reader = new StreamReader("BookData.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] line = reader.ReadLine().Split('`');
                        int ID = int.Parse(line[0]);
                        string Name = line[1];
                        int Date = int.Parse(line[2]);
                        Subject subject = (Subject)Enum.Parse(typeof(Subject), line[3]);
                        int Count = int.Parse(line[4]);
                        int BorrowedID = int.Parse(line[5]);
                        if (line[6] != string.Empty && line[6] != null)
                        {
                            ReservedMemberIDs = line[6].Split(',').Select(x => int.Parse(x)).ToList();
                        }
                        Book book = new Book(Name, ID, subject, Date, Count, new List<int>());
                        book.Mem_Ids_Reserve = ReservedMemberIDs;
                        book.BorrowedID = BorrowedID;
                        Books.Add(book);
                    }
                }
                using (StreamReader reader = new StreamReader("MemberData.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string[] line = reader.ReadLine().Split('`');
                        int ID = int.Parse(line[0]);
                        string Name = line[1];
                        string Username = line[2];
                        string Password = line[3];
                        if (line[4] != string.Empty && line[4] != null)
                        {
                            BorrowedBookIDs = line[4].Split(',').Select(x => int.Parse(x)).ToList();
                        }
                        if (line[5] != string.Empty && line[5] != null)
                        {
                            ReservedBookIDs = line[5].Split(',').Select(x => int.Parse(x)).ToList();
                        }
                        Member member = new Member(Username, Password, Name, ID, 0, new List<int>(), new List<int>());
                        member.Book_ids_Borrow = BorrowedBookIDs;
                        member.Book_ids_Reserve = ReservedBookIDs;
                        Members.Add(member);
                    }
                }

            }
            catch { }
        }
    }
    public abstract class BaseClass
    {
        private string _name = "";
        private int _id;
        public string Name { get { return _name; } set { _name = value; } }
        public BaseClass(string Name, int ID)
        {
            this.Name = Name;
            this.ID = ID;
        }
        public int ID { get { return _id; } set { _id = value; } }
        virtual public void Register() { }
        virtual public void Delete() { }
        virtual public string Display() { return string.Empty; }
        virtual public void Modify(BaseClass baseClass) { }
    }
    public class Member : BaseClass
    {
        static public List<Member> Members = new List<Member>();
        private int _book_number;
        private string _username;
        private string _password;
        public string Username { get { return _username; } set { _username = value; } }
        public string Password { get { return _password; } set { _password = value; } }
        public int Book_number { get { return _book_number; } set { _book_number = value; } }
        public List<int> Book_ids_Reserve;
        public List<int> Book_ids_Borrow;
        public Member(string Username, string Password, string Name, int ID, int Book_number, List<int> Book_ids_Reserve, List<int> Book_ids_Borrow) : base(Name, ID)
        {
            this.Username = Username;
            this.Password = Password;
            this.Book_number = Book_number;
            this.Book_ids_Reserve = Book_ids_Reserve;
            this.Book_ids_Borrow = Book_ids_Borrow;
        }
        public bool Borrow(ref Book book)
        {
            if (Book_number < 5)
            {
                Book_ids_Borrow.Add(book.ID);
                Book_number++;
                book.Count--;
                book.BorrowedID = ID;
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Return(Book book)
        {
            Book_ids_Borrow.Remove(book.ID);
            Book_number--;
            book.Count++;
            book.BorrowedID = 0;
        }
        public List<Book> Books_Reserved()
        {
            List<Book> books = new List<Book>();
            for (int i = 0; i < Book.Books.Count; i++)
            {
                foreach (var id in Book_ids_Reserve)
                {
                    if (Book.Books[i].ID == id)
                    {
                        books.Add(Book.Books[i]);
                    }
                }
            }
            return books;
        }
        public static Member Search(int ID)
        {
            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i].ID == ID)
                {
                    return Members[i];
                }
            }
            return null;
        }
        public static Member Search(string Name)
        {
            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i].Name == Name)
                {
                    return Members[i];
                }
            }
            return null;
        }
        public static Member Find(string Username)
        {
            for (int i = 0; i < Members.Count; i++)
            {
                if (Members[i].Username == Username)
                {
                    return Members[i];
                }
            }
            return null;
        }
        public override void Register()
        {
            Members.Add(this);
        }
        public override void Delete()
        {
            Members.Remove(this);
        }
        public override string Display()
        {
            return string.Empty;
        }
        public override void Modify(BaseClass baseClass)
        {
            Member member = (Member)baseClass;
            Name = member.Name;
            Username = member.Username;
            Password = member.Password;
        }
    }
    public class Book : BaseClass
    {
        public static List<Book> Books = new List<Book>();
        private Subject _subject;
        private int _date;
        private int _count;
        public List<int> Mem_Ids_Reserve;
        public int BorrowedID;
        public Subject Subject { get { return _subject; } set { _subject = value; } }
        public int Date { get { return _date; } set { _date = value; } }
        public int Count { get { return _count; } set { _count = value; } }
        public Book(string Name, int ID, Subject Subject, int Date, int Count, List<int> Mem_Ids_Reserve) : base(Name, ID)
        {
            this.Subject = Subject;
            this.Date = Date;
            this.Count = Count;
            this.Mem_Ids_Reserve = Mem_Ids_Reserve;
        }
        public void Reserve(Member member)
        {
            Mem_Ids_Reserve.Add(member.ID);
            member.Book_ids_Reserve.Add(ID);
        }
        public void Return(Member member)
        {
            member.Book_number--;
        }
        public static Book Search(int ID)
        {
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].ID == ID)
                {
                    return Books[i];
                }
            }
            return null;
        }
        public static Book Search(string Name)
        {
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].Name == Name)
                {
                    return Books[i];
                }
            }
            return null;
        }
        public static Book Search(Subject Subject)
        {
            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].Subject == Subject)
                {
                    return Books[i];
                }
            }
            return null;
        }
        public override void Register()
        {
            Books.Add(this);
        }
        public override void Delete()
        {
            Books.Remove(this);
        }
        public override string Display() => $"ID: {ID}, Name: {Name}, DATE: {Date}, SUBJECT: {Subject}";
        public override void Modify(BaseClass baseClass)
        {
            Book book = (Book)baseClass;
            Name = book.Name;
            Date = book.Date;
            Subject = book.Subject;
        }
    }
}
