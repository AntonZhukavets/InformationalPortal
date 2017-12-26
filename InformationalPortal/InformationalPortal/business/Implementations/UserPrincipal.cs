using InformationalPortal.business.Interfaces;
using InformationalPortal.Models;
using InfPortal.business.DTO;
using System.Security.Principal;
using InfPortal.business.Providers;
using InfPortal.business.Interfaces;


namespace InformationalPortal.business.Implementations
{
    public class UserPrincipal : IPrincipal
    {
        private IIdentity identity;
        private IAuthenticationUser authUser;
        
        public UserPrincipal(IAuthenticationUser authUser)
        {
            this.authUser = authUser;
            identity = new GenericIdentity(authUser.Id.ToString());   
        }
        public IIdentity Identity
        {            
            get { return identity; }
        }
        public bool IsInRole(string role)
        {
            if (authUser.Id == 0)
            {
                return false;
            }
            return authUser.RoleName == role;
        }
        public override string ToString()
        {
            if (authUser.Id == 0)
            {
                return string.Empty;
            }
            return authUser.Login;
        }
    }
}