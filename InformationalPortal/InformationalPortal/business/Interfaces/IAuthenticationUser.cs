namespace InformationalPortal.business.Interfaces
{
    public interface IAuthenticationUser
    {
        string RoleName { get; set; }
        int Id { get; set; }
        string Login { get; set; }
    }
}
