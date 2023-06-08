namespace App.Dtos
{
      public class ConfirmEmailRequest
      {
            public string ToEmail { get; set; } = string.Empty;
            public string UserName { get; set; } = string.Empty;
            public string ComfirmEmailLink { get; set; } = string.Empty;
            public string? Content { get; set; } = string.Empty;
      }
}
