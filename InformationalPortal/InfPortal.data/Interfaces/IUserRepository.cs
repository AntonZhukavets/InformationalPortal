using InfPortal.data.Entities;

namespace InfPortal.data.Interfaces
{
    public interface IUserRepository
    {
        bool IsValidUser(string login, string password);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int? id);
        bool ResumeUser(int? id);
        bool MakeAdmin(int? id);
        User GetUserByLoginAndPassword(string login, string password);
        User GetUserById(int? id);        
        User[] GetAllUsers();
    }
}
