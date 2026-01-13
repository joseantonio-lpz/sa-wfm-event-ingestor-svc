namespace WFM.EventIngestor.Application.Interfaces
{
    public interface IUserValidationService
    {
        Task<bool> ValidateUserAsync(string username, string password);
    }
}