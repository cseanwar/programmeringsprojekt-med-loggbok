using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Services;

namespace LibraryManagement
{
    public class MainMenu
    {
        private readonly LibraryContext _context;
        private readonly UserService _userService;
        private readonly BookService _bookService;
        private readonly LoanService _loanService;

        public MainMenu(LibraryContext context, UserService userService, BookService bookService, LoanService loanService)
        {
            _context = context;
            _userService = userService;
            _bookService = bookService;
            _loanService = loanService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Library Management System =====");
                Console.WriteLine("1.  Register User");
                Console.WriteLine("2.  View all the User");
                Console.WriteLine("3.  Login");
                Console.WriteLine("4.  Add Book");
                Console.WriteLine("5.  Sort Books by Title");
                Console.WriteLine("6.  View all the Books");
                Console.WriteLine("7.  Update Books");
                Console.WriteLine("8.  Delete a Book");
                Console.WriteLine("9.  Search Books");
                Console.WriteLine("10. Borrow Book");
                Console.WriteLine("11. Return Book");
                Console.WriteLine("12. Exit");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1": RegisterUser(); break;
                    case "2": ViewUsers(); break;
                    case "3": Login(); break;
                    case "4": AddBook(); break;
                    case "5": SortBooksByTitle(); break;
                    case "6": ViewBooks(); break;
                    case "7": UpdateBook(); break;
                    case "8": DeleteBook(); break;
                    case "9": SearchBooksByTitle(); break;
                    case "10": BorrowBook(); break;
                    case "11": ReturnBook(); break;
                    case "12": return;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // TODO: implement methods
        // 1. Register a User in the Database
        private void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("===== Register New User =====");

            Console.Write("\nEnter name: ");
            string name = Console.ReadLine()?.Trim();

            Console.Write("Enter email: ");
            string email = Console.ReadLine()?.Trim();

            Console.Write("Enter password: ");
            string password = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("\nName, Email and Password cannot be empty!");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                Console.ReadKey();
                return;
            }

            // Check if user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);

            if (existingUser != null)
            {
                Console.WriteLine("\nA user with this email already exists.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                Console.ReadKey();
                return;
            }

            var newUser = new User
            {
                Name = name,
                Email = email,
                Password = password
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

            Console.WriteLine("\nUser registered successfully!");
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // View all the Users in the Database
        private void ViewUsers()
        {
            Console.Clear();
            Console.WriteLine("===== Registered Users =====");

            var users = _context.Users.ToList();

            if (users.Count == 0)
            {
                Console.WriteLine("\nNo users found.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Password: {user.Password}");
                    Console.WriteLine("---------------------------");
                }
            }

            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadLine();
        }

        // Login in the Library
        private void Login()
        {
            Console.Clear();
            Console.WriteLine("===== Login =====");

            Console.Write("\nEnter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                Console.WriteLine("\nNo user found with that email.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                Console.ReadKey();
                return;
            }

            if (user.Password != password)
            {
                Console.WriteLine("\nOops! Incorrect password.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"\nLogin successful! Welcome {user.Name}");
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // Add a book in the Library 
        private void AddBook()
        {
            Console.Clear();
            Console.WriteLine("===== Add a Book in the Library =====");
            Console.Write("\nEnter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();

            _bookService.AddBook(title, author, isbn);

            Console.WriteLine("\nBook added.");
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // Sort all the books according to title (Ascending/Descending)
        private void SortBooksByTitle()
        {
            Console.Clear();
            Console.WriteLine("===== Sort all the Books in the Library =====");
            Console.Write("\nSort ascending (A-Z) or descending (Z-A)? (a/d): ");
            string option = Console.ReadLine().ToLower();

            bool ascending = option == "a";

            var sortedBooks = _bookService.SortByTitle(ascending);

            Console.WriteLine("\n--- Books Sorted by Title ---");
            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
            }
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // View all the books in the library in a table
        private void ViewBooks()
        {
            Console.Clear();
            Console.WriteLine("===== View all the Books in the Library =====");

            var books = _bookService.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            }
            else
            {
                _bookService.ViewBooksInTable(_bookService.GetAllBooks());
            }
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // Search a book in the library by title
        private void SearchBooksByTitle()
        {
            Console.Clear();
            Console.WriteLine("===== Search a Book in the Library =====");
            Console.Write("\nEnter title to search: ");
            string title = Console.ReadLine();

            var results = _bookService.SearchByTitle(title);

            if (results.Count == 0)
            {
                Console.WriteLine("\nNo books found.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                return;
            }

            Console.WriteLine("\n--- Search Results ---");
            foreach (var book in results)
            {
                Console.WriteLine($"{book.Title} by {book.Author} (ISBN: {book.ISBN})");
            }
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
        }

        // Update books
        private void UpdateBook()
        {
            Console.Clear();
            Console.WriteLine("===== Update a Book in the Library =====");
            Console.Write("\nEnter the existing book title: ");
            string oldTitle = Console.ReadLine();

            var book = _bookService.GetBookByTitle(oldTitle);
            if (book == null)
            {
                Console.WriteLine("\nBook not found.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                return;
            }

            Console.Write("\nNew title: ");
            string newTitle = Console.ReadLine();

            Console.Write("New author: ");
            string author = Console.ReadLine();

            Console.Write("New ISBN: ");
            string isbn = Console.ReadLine();

            _bookService.UpdateBook(oldTitle, newTitle, author, isbn);

            Console.WriteLine("\nBook updated.");
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // Remove a book from the library
        private void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("===== Remove a Book from the Library =====");
            Console.Write("\nEnter title of book to delete: ");
            string title = Console.ReadLine();

            if (_bookService.DeleteBook(title))
                Console.WriteLine("\nBook deleted.");
            else
                Console.WriteLine("\nBook not found.");
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
        }

        // Borrow a book from the library
        private void BorrowBook()
        {
            Console.Clear();
            Console.WriteLine("===== Borrow a Book from the Library =====");
            Console.Write("\nEnter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter book title: ");
            string title = Console.ReadLine();

            var book = _bookService.GetBookByTitle(title);

            if (book == null)
            {
                Console.WriteLine("\nBook not found.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                return;
            }

            string result = _loanService.BorrowBook(username, book);
            Console.WriteLine(result);
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        // Return a book to the library
        private void ReturnBook()
        {
            Console.Clear();
            Console.WriteLine("===== Return a Book to the Library =====");
            Console.Write("\nEnter username: ");
            string username = Console.ReadLine();

            Console.Write("Enter book title: ");
            string title = Console.ReadLine();

            var book = _bookService.GetBookByTitle(title);

            if (book == null)
            {
                Console.WriteLine("\nBook not found.");
                Console.WriteLine("\nPress ENTER to return to the Main Manu...");
                return;
            }

            string result = _loanService.ReturnBook(username, book);
            Console.WriteLine(result);
            Console.WriteLine("\nPress ENTER to return to the Main Manu...");
            Console.ReadKey();
        }
    }
}
