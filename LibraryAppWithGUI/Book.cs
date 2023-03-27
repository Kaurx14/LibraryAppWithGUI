using System;
namespace LibraryAppWithGUI
{
    public class Book
    {
        public string title;
        public string author;
        public string isbn;

        public Book(string title, string author, string isbn)
        {
            this.title = title;
            this.author = author;
            this.isbn = isbn;
        }
    }
}