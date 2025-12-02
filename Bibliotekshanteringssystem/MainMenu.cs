using LibraryManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement
{
    public class MainMenu
    {
        private readonly LibraryContext _context;

        public MainMenu(LibraryContext context)
        {
            _context = context;
        }

        public void Show()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== Library Management System =====");
                Console.WriteLine("1. Register User");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Add Book");
                Console.WriteLine("4. Borrow Book");
                Console.WriteLine("5. Return Book");
                Console.WriteLine("6. Search Books");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        RegisterUser();
                        break;
                    case "2":
                        Login();
                        break;
                    case "3":
                        AddBook();
                        break;
                    case "4":
                        BorrowBook();
                        break;
                    case "5":
                        ReturnBook();
                        break;
                    case "6":
                        SearchBooks();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // TODO: implement methods
        private void RegisterUser() { }
        private void Login() { }
        private void AddBook() { }
        private void BorrowBook() { }
        private void ReturnBook() { }
        private void SearchBooks() { }
    }
}

