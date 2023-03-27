using System;
namespace LibraryAppWithGUI
{
    public class User
    {
        public string username;
        public string id;

        public User(string username, string id)
        {
            this.username = username;
            this.id = id;
        }

        public void BorrowedBooks(List<BorrowedBook> borrowedBooks)
        {
            List<BorrowedBook> filteredList = borrowedBooks.Where(borrowedBook => borrowedBook.user == this).ToList();

            if (filteredList.Count == 0)
            {
                Console.WriteLine("You have not borrowed any books");
            }
            else
            {
                foreach (BorrowedBook book in filteredList)
                {
                    Console.WriteLine($"ISBN: {book.book.isbn}, borrowed on {book.borrowDate.ToString("MM/dd/yyyy")}");
                }
            }
        }
    }
}

