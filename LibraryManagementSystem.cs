using System;
using System.Collections.Generic;
using System.Linq;
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine("Book added successfully!");
        }

        public void ViewBooks()
        {
            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Status: {(book.IsBorrowed ? "Borrowed" : "Available")}");
            }
        }

        public void BorrowBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
            }
            else if (book.IsBorrowed)
            {
                Console.WriteLine("Book is already borrowed.");
            }
            else
            {
                book.IsBorrowed = true;
                Console.WriteLine("Book borrowed successfully.");
            }
        }

        public void ReturnBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
            }
            else if (!book.IsBorrowed)
            {
                Console.WriteLine("Book was not borrowed.");
            }
            else
            {
                book.IsBorrowed = false;
                Console.WriteLine("Book returned successfully.");
            }
        }
    }

   
            