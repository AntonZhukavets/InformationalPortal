using InfPortal.business.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.business.Interfaces
{
    public interface IUserProvider
    {
        bool IsValidUser(string login, string password);
        UserDTO GetUserByLogin(string login);
        UserDTO GetUserById(int? id);
        void Registration(UserDTO user);
        void UpdateUser(UserDTO user);
    }
}
