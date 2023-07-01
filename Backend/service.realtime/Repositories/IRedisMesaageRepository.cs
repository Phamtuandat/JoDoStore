namespace RealtimeApp.Repositories;
public interface IRedisMesaageRepository
{
      Task AddMessageToQueue(string queueName, string message);
      Task<string> GetNextMessageFromQueue(string queueName);
      Task<List<string>> GetAllMessagesFromQueue(string queueName);
}