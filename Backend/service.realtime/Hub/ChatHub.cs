namespace RealtimeApp.Hub;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using RealtimeApp.Repositories;

public class ChatHub : Hub
{
      private readonly IConnectRepository _connect;
      private readonly ILogger<ChatHub> _logger;

      public ChatHub(IConnectRepository connect, ILogger<ChatHub> logger)
      {
            _connect = connect;
            _logger = logger;
      }

      [Authorize]
      public async Task OnConnect(string userId)
      {
            await _connect.AddToListAsync(userId, this.Context.ConnectionId);
            await Clients.Client(this.Context.ConnectionId).SendAsync("Welcome to back to chatHub" + userId);
      }
      [Authorize]
      public async Task OnDisconnect(string userId)
      {
            await _connect.RemoveFromListAsync(userId);
      }
      [Authorize]
      public async Task SendMessage(string userId, string message)
      {
            await Clients.All.SendAsync("ReceiveMessage", userId, message);
      }

}