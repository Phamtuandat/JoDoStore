
namespace RealtimeApp.Models.Chathub;
public class Room
{
      public string Id { get; set; }
      public string Name { get; set; }
      public ICollection<Message> Messages { get; set; }
      public DateTime Created { get; set; }
      public ICollection<User> Members { get; set; }
      public ICollection<User> Administrators { get; set; }
}