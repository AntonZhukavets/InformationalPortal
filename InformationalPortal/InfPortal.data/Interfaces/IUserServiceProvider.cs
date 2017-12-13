using InfPortal.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfPortal.data.Interfaces
{
    public interface IUserServiceProvider
    {
        bool IsValidUser(string login, string password);
        User GetUserByLogin(string login);
        User GetUserById(int? id);
        void Registration(User user);
        void UpdateUser(User user);
    }
}
