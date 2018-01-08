namespace InfPortal.service.Business.Logs
{
    public interface IServiceLoger
    {
        void Info(string message);
        void Error(string message);
        void Warning(string message);
    }
}
