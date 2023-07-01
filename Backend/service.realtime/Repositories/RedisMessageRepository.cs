namespace RealtimeApp.Repositories;
using StackExchange.Redis;

public class RedisMessageRepository : IRedisMesaageRepository
{
      private readonly IConnectionMultiplexer _redis;

      public RedisMessageRepository(IConnectionMultiplexer connectionMultiplexer)
      {
            _redis = connectionMultiplexer;
      }

      public async Task AddMessageToQueue(string queueName, string message)
      {
            var db = _redis.GetDatabase();
            await db.ListRightPushAsync(queueName, message);
      }

      public async Task<string> GetNextMessageFromQueue(string queueName)
      {
            var db = _redis.GetDatabase();
            var message = await db.ListLeftPopAsync(queueName);
            return message;
      }

      public async Task<List<string>> GetAllMessagesFromQueue(string queueName)
      {
            var db = _redis.GetDatabase();
            var messages = await db.ListRangeAsync(queueName);
            return messages.Select(x => x.ToString()).ToList();
      }
}
