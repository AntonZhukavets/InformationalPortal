using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using InfPortal.business.Implementations;
using InfPortal.business.Interfaces;

namespace InformationalPortal.business.Implementations
{
    public class UserPrincipal : IPrincipal
    {
        private IIdentity identity;

        private IUserProvider userProvider;
        private int id;
        public UserPrincipal(int id)
        {
            if(id==0)
            {
                identity = new GenericIdentity(string.Empty);
            }
            identity = new GenericIdentity(id.ToString());
            userProvider = new UserProvider();
            this.id = id;
        }
        public IIdentity Identity
        {
            get { return identity; }
        }

        public bool IsInRole(string role)
        {
            return userProvider.GetUserById(id).role.Name == role;
        }
        public override string ToString()
        {
            if(id==0)
            {
                return string.Empty;
            }
            string login=userProvider.GetUserById(id).Login;
            return login;
        }
    }
}