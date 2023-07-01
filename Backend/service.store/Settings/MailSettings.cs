namespace App.Settings
{
      public class EmailSettings
      {
            public string UserName { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string SmtpServer { get; set; } = string.Empty;
            public int Port { get; set; }
      }
}
