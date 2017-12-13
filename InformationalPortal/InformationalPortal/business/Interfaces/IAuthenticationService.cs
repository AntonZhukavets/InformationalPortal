using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformationalPortal.business.Enums;

namespace InformationalPortal.business.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string login, string password);
        void Logout();
    }
}
