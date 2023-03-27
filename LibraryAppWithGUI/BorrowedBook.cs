using System;
namespace LibraryAppWithGUI
{
    public class BorrowedBook
    {
        public Book book;
        public DateTime borrowDate;
        public User user;

        public BorrowedBook(Book book, DateTime borrowDate, User user)
        {
            this.book = book;
            this.borrowDate = borrowDate;
            this.user = user;
        }
    }
}

