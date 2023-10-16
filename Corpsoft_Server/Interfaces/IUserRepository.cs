
using Corpsoft_Server.Model;

namespace URL_Shortener_Server.Interfaces
{
    public interface IUserRepository
    {
        User GetUserById(int id);
        User GetUserByUsername(string username);
        void AddUser(User user);

        ICollection<User> GetUsers();
    }   
}