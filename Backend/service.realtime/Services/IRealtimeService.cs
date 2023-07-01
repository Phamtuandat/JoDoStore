using RealtimeApp.Models.Chathub;

namespace RealtimeApp.Services;
public interface IRealtimeService
{
      Task<Message> GetMessageAsync(string roomId);
}