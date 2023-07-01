namespace RealtimeApp.Models;

public class RealtimeDbSettings : IRealtimeDbSettings
{
      public string RoomsCollectionName { get; set; }
      public string MessagesCollectionName { get; set; }
      public string MembersCollectionName { get; set; }
      public string ConnectionString { get; set; }
      public string DatabaseName { get; set; }
}
public interface IRealtimeDbSettings
{
      string RoomsCollectionName { get; set; }
      string MessagesCollectionName { get; set; }
      string MembersCollectionName { get; set; }
      string ConnectionString { get; set; }
      string DatabaseName { get; set; }
}