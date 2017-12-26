using InformationalPortal.business.Interfaces;


namespace InformationalPortal.business.Implementations
{
    public class AuthenticationUser : IAuthenticationUser
    {
        private string roleName;
        private int id;
        private string login;
        public string RoleName
        {
            get { return this.roleName; }
            set { this.roleName=value; }
        }
        public int Id
        {
            get { return this.id; }
            set { this.id=value; }
        }
        public string Login
        {
            get { return this.login; }
            set { this.login = value; }
        }
    }
}