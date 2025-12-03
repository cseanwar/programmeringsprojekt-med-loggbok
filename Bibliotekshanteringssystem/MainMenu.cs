using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement
{
    public class MainMenu
    {
        private readonly LibraryContext _context;

        public MainMenu(LibraryContext context)
        {
            _context = context;
        }

        private readonly BookService _bookService;

        public MainMenu(LibraryContext context, BookService bookService)
        {
            _context = context;
            _bookService = bookService;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Library Management System =====");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. View all the User");
                Console.WriteLine("3. Login");
                Console.WriteLine("4. Add Book");
                Console.WriteLine("5. View all the Books");
                Console.WriteLine("6. Update Books");
                Console.WriteLine("7. Delete a Book");
                Console.WriteLine("8. Search Books");
                Console.WriteLine("9. Borrow Book");
                Console.WriteLine("10. Return Book");
                Console.WriteLine("11. Exit");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        ViewUsers();     
                        break;
                    case "3":
                        Login();
                        break;
                    case "4":
                        AddBook();
                        break;
                    case "5":
                        ViewBooks();
                        break;
                    case "6":
                        UpdateBook();
                        break;
                    case "7":
                        DeleteBook();
                        break;
                    case "8":
                        SearchBooks();
                        break;
                    case "9":
                        BorrowBook();
                        break;
                    case "10":
                        ReturnBook();
                        break;
                    case "11":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // TODO: implement methods
        private void RegisterUser()
        {
            Console.Clear();
            Console.WriteLine("===== Register New User =====");

            Console.Write("Enter name: ");
            string name = Console.ReadLine()?.Trim();

            Console.Write("Enter email: ");
            string email = Console.ReadLine()?.Trim();

            Console.Write("Enter password: ");
            string password = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Name, Email and Password cannot be empty!");
                Console.ReadKey();
                return;
            }

            // Check if user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == email);

            if (existingUser != null)
            {
                Console.WriteLine("A user with this email already exists.");
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

            Console.WriteLine("User registered successfully!");
            Console.ReadKey();
        }

        private void ViewUsers()
        {
            Console.Clear();
            Console.WriteLine("===== Registered Users =====");

            var users = _context.Users.ToList();

            if (users.Count == 0)
            {
                Console.WriteLine("No users found.");
            }
            else
            {
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.Id}, Name: {user.Name}, Email: {user.Email}, Password: {user.Password}");
                    Console.WriteLine("---------------------------");
                }
            }

            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }

        private void Login()
        {
            Console.Clear();
            Console.WriteLine("===== Login =====");

            Console.Write("Enter email: ");
            string email = Console.ReadLine();

            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                Console.WriteLine("No user found with that email.");
                Console.WriteLine("Press ENTER to return to the Main Manu...");
                Console.ReadKey();
                return;
            }

            if (user.Password != password)
            {
                Console.WriteLine("Oops! Incorrect password.");
                Console.WriteLine("Press ENTER to return to the Main Manu...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Login successful! Welcome {user.Name}");
            Console.WriteLine("Press ENTER to return to the Main Manu...");
            Console.ReadKey();
        }

        private void AddBook()
        {
            Console.Clear();
            Console.WriteLine("===== Add New Book =====");

            Console.Write("Enter title: ");
            string title = Console.ReadLine();

            Console.Write("Enter author: ");
            string author = Console.ReadLine();

            Console.Write("Enter ISBN: ");
            string isbn = Console.ReadLine();

            _bookService.AddBook(title, author, isbn);

            Console.WriteLine("Book added successfully!");
            Console.ReadKey();
        }

        private void ViewBooks()
        {
            Console.Clear();
            Console.WriteLine("===== All Books =====");

            var books = _bookService.GetAllBooks();

            if (books.Count == 0)
            {
                Console.WriteLine("No books available.");
            }
            else
            {
                foreach (var b in books)
                {
                    Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, ISBN: {b.ISBN}");
                    Console.WriteLine("-----------------------");
                }
            }

            Console.ReadKey();
        }

        private void SearchBooks()
        {
            Console.Clear();
            Console.WriteLine("===== Search Books =====");
            Console.Write("Enter keyword: ");

            string keyword = Console.ReadLine();
            var results = _bookService.SearchBooks(keyword);

            if (results.Count == 0)
                Console.WriteLine("No matching books found.");
            else
            {
                foreach (var b in results)
                    Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, ISBN: {b.ISBN}");
            }

            Console.ReadKey();
        }


        private void UpdateBook()
        {
            Console.Clear();
            Console.WriteLine("===== Update Book =====");

            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());

            var book = _bookService.GetBookById(id);
            if (book == null)
            {
                Console.WriteLine("Book not found!");
                Console.ReadKey();
                return;
            }

            Console.Write("New title: ");
            string title = Console.ReadLine();

            Console.Write("New author: ");
            string author = Console.ReadLine();

            Console.Write("New ISBN: ");
            string isbn = Console.ReadLine();

            _bookService.UpdateBook(id, title, author, isbn);

            Console.WriteLine("Book updated successfully!");
            Console.ReadKey();
        }

        private void DeleteBook()
        {
            Console.Clear();
            Console.WriteLine("===== Delete Book =====");

            Console.Write("Enter Book ID: ");
            int id = int.Parse(Console.ReadLine());

            if (_bookService.DeleteBook(id))
                Console.WriteLine("Book deleted successfully.");
            else
                Console.WriteLine("Book not found.");

            Console.ReadKey();
        }

        private void BorrowBook() { }
        private void ReturnBook() { }
    }
}

