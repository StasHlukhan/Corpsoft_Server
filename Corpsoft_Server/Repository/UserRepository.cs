using Corpsoft_Server.Data;
using Corpsoft_Server.Model;
using URL_Shortener_Server.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }

    public User GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(u => u.Id == id);
    }

    public User GetUserByUsername(string username)
    {
        return _context.Users.FirstOrDefault(u => u.Username == username);
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public ICollection<User> GetUsers()
    {
        return _context.Users.ToList();
    }
}