namespace RealtimeApp.Repositories;

public interface IConnectRepository
{
      Task AddToListAsync(string userId, string connectionId);
      Task<bool> RemoveFromListAsync(string userId);
      Task<bool> IsConnectingAsync(string userId);
}