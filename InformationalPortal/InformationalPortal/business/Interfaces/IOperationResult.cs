namespace InformationalPortal.business.Interfaces
{
    public interface IOperationResult
    {
        bool ResultOfOperation { get; set; }
        string Message { get; set; }
        string Redirect { get; set; }
    }
}
