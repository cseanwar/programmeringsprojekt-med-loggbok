using LibraryManagement.Models;
using LibraryManagement.Data;

namespace LibraryManagement.Services
{
    public class UserService
    {
        private readonly LibraryContext _context;

        public UserService(LibraryContext context)
        {
            _context = context;
        }

        public void AddUser(string name, string email)
        {
            var user = new User { Name = name, Email = email };
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public User? GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
