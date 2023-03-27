using System;

namespace LibraryAppWithGUI
{
    public class Library
    {
        public List<Book> books;
        public List<BorrowedBook> borrowedBooks;

        public Library(List<Book> books, List<BorrowedBook> borrowedBooks)
        {
            this.books = books;
            this.borrowedBooks = borrowedBooks;
        }

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public void RemoveBook(string isbn)
        {
            var foundBook = books.Where(book => book.isbn == isbn);

            int countOfBooksFound = books.RemoveAll(book => book.isbn == isbn);


            // Needs functionality to remove book from UI aswell
            // Rework this bit of code
            if (countOfBooksFound == 0)
            {
                Console.WriteLine("Book: \"" + foundBook.ToString() + "\" not found");
            }
            else
            {
                Console.WriteLine("Book: \"" + foundBook.ToString() + "\" removed from the library");
            }
        }

        public void SearchBooks(string keyword)
        {
            var foundBooks = books.Where(book => book.title.Contains(keyword));
            List<Book> foundBooksList = new List<Book>();
            if (foundBooks == null)
            {
                Console.WriteLine("No books were found");
            }
            else
            {
                foreach (Book book in foundBooks)
                {
                    foundBooksList.Add(book);
                }

                Console.WriteLine("Found books: ");
                DisplayBooks(foundBooksList);
            }
        }

        public void DisplayBooks(List<Book> booksToDisplay)
        {
            foreach (Book book in booksToDisplay)
            {
                Console.WriteLine("\nTitle: " + book.title);
                Console.WriteLine("Author: " + book.author);
                Console.WriteLine("ISBN: " + book.isbn);
                Console.WriteLine("");
            }
        }

        public void BorrowBook(string isbn, User user)
        {
            bool isUnavailable = false;

            foreach (BorrowedBook borrowedBook in borrowedBooks)
            {
                if (borrowedBook.book.isbn == isbn)
                    isUnavailable = true;
            }

            if (isUnavailable)
            {
                Console.WriteLine("Book is already borrowed");
            }
            else
            {
                Book bookToBeBorrowed = null;
                foreach (Book book in books)
                {
                    if (book.isbn == isbn)
                        bookToBeBorrowed = book;
                }
                BorrowedBook borrowedBook = new BorrowedBook(bookToBeBorrowed, DateTime.Now, user);
                borrowedBooks.Add(borrowedBook);

            
                for (int i = 0; i < books.Count; i++)
                {
                    if (books[i].isbn == isbn)
                        books.Remove(books[i]);
                }
            }
        }

        public void ReturnBook(string isbn)
        {
            bool isBorrowed = false;
            foreach (BorrowedBook borrowedBook in borrowedBooks)
            {
                if (borrowedBook.book.isbn == isbn)
                    isBorrowed = true;
            }

            if (isBorrowed)
            {
                for (int i = 0; i < borrowedBooks.Count; i++)
                {
                    if (borrowedBooks[i].book.isbn == isbn)
                    {
                        books.Add(borrowedBooks[i].book);
                        borrowedBooks.Remove(borrowedBooks[i]);
                    }
                }
            }
            else
            {
                Console.WriteLine("The book is not currently borrowed");
            }
        }
    }
}
