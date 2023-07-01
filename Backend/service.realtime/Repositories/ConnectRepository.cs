using StackExchange.Redis;

namespace RealtimeApp.Repositories;

public class ConnectRepository : IConnectRepository
{
      private readonly IConnectionMultiplexer _redis;
      private readonly ILogger<ConnectRepository> _logger;

      public ConnectRepository(IConnectionMultiplexer redis, ILogger<ConnectRepository> logger)
      {
            _redis = redis;
            _logger = logger;
      }

      public async Task AddToListAsync(string userId, string connectionId)
      {
            var db = _redis.GetDatabase();
            var connected = await db.StringGetAsync(userId);
            if (connected.ToString() != string.Empty)
            {
                  if (connected.ToString() != connectionId)
                  {
                        await db.StringSetAsync(userId, connectionId);
                        return;
                  }
                  _logger.LogInformation("user is connecting with connection " + connectionId);
            }
            await db.StringSetAsync(userId, connectionId);
      }

      public async Task<bool> IsConnectingAsync(string userId)
      {
            var db = _redis.GetDatabase();
            var connected = await db.StringGetAsync(userId);
            if (connected.ToString() != string.Empty)
            {
                  return true;
            }
            return false;
      }

      public async Task<bool> RemoveFromListAsync(string userId)
      {
            bool isRemoved = false;
            var db = _redis.GetDatabase();
            var connected = await db.StringGetAsync(userId);
            if (connected.ToString() != string.Empty)
            {
                  isRemoved = db.KeyDelete(userId);
            }
            return isRemoved;
      }
}