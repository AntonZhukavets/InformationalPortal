using InformationalPortal.business.Enums;


namespace InformationalPortal.business.Interfaces
{
    public interface IAuthenticationService
    {
        AuthenticationResult Login(string login, string password);
        void Logout();
        int UserId { get; }
    }
}
