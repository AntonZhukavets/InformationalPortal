using InfPortal.business.DTO;


namespace InfPortal.business.Interfaces
{
    public interface IUserProvider
    {
        bool Registration(UserDTO user);
        bool UpdateUser(UserDTO user);
        bool DeleteUser(int? id);
        bool ResumeUser(int? id);
        bool MakeAdmin(int? id);
        bool IsValidUser(string login, string password);
        UserDTO GetUserByLoginAndPassword(string login, string password);
        UserDTO GetUserById(int? id);
        UserDTO[] GetAllUsers();
               
    }
}
