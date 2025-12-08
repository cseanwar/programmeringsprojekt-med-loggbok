using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public class BookService
    {
        private List<Book> _books = new List<Book>();

        public void AddBook(string title, string author, string isbn)
        {
            _books.Add(new Book
            {
                Title = title,
                Author = author,
                ISBN = isbn,
                IsBorrowed = false
            });
        }
        public List<Book> SortByTitle(bool ascending = true)
        {
            if (ascending)
                return _books.OrderBy(b => b.Title).ToList();
            else
                return _books.OrderByDescending(b => b.Title).ToList();
        }

        public List<Book> GetAllBooks()
        {
            return _books;
        }
        public void ViewBooksInTable(List<Book> books)
        {
            if (books == null || books.Count == 0)
            {
                Console.WriteLine("No books found.");
                return;
            }

            Console.WriteLine("---------------------------------------------------------------------");
            Console.WriteLine($"{"Title",-30} {"Author",-20} {"ISBN",-15} {"Borrowed"}");
            Console.WriteLine("---------------------------------------------------------------------");

            foreach (var b in books)
            {
                Console.WriteLine($"{b.Title,-30} {b.Author,-20} {b.ISBN,-15} {b.IsBorrowed}");
            }

            Console.WriteLine("---------------------------------------------------------------------");
        }


        public Book? GetBookByTitle(string title)
        {
            return _books.FirstOrDefault(b =>
                b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Book> SearchByTitle(string title)
        {
            return _books
                .Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void UpdateBook(string oldTitle, string newTitle, string author, string isbn)
        {
            var book = GetBookByTitle(oldTitle);
            if (book == null) return;

            book.Title = newTitle;
            book.Author = author;
            book.ISBN = isbn;
        }

        public bool DeleteBook(string title)
        {
            var book = GetBookByTitle(title);
            if (book == null) return false;

            _books.Remove(book);
            return true;
        }
    }
}
