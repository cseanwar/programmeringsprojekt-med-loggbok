using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class BookService
    {
        private readonly List<Book> _books = new List<Book>();
        private int _nextId = 1;

        // Add new book
        public void AddBook(string title, string author, string isbn)
        {
            var book = new Book
            {
                Id = _nextId++,
                Title = title,
                Author = author,
                ISBN = isbn
            };

            _books.Add(book);
        }

        // View all books
        public List<Book> GetAllBooks()
        {
            return _books;
        }

        // Search books by title or author
        public List<Book> SearchBooks(string keyword)
        {
            keyword = keyword.ToLower();
            return _books.Where(b =>
                b.Title.ToLower().Contains(keyword) ||
                b.Author.ToLower().Contains(keyword)
            ).ToList();
        }

        // Find book by ID
        public Book GetBookById(int id)
        {
            return _books.FirstOrDefault(b => b.Id == id);
        }

        // Update book
        public bool UpdateBook(int id, string title, string author, string isbn)
        {
            var book = GetBookById(id);
            if (book == null)
                return false;

            book.Title = title;
            book.Author = author;
            book.ISBN = isbn;
            return true;
        }

        // Delete book
        public bool DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book == null)
                return false;

            _books.Remove(book);
            return true;
        }
    }
}
