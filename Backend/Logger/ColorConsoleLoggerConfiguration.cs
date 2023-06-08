namespace App
{
      public sealed class ColorConsoleLoggerConfiguration
      {
            public int EventId { get; set; }

            public Dictionary<LogLevel, ConsoleColor> LogLevels { get; set; } = new()
            {
                  [LogLevel.Information] = ConsoleColor.Cyan,
                  [LogLevel.Error] = ConsoleColor.DarkRed,
                  [LogLevel.Warning] = ConsoleColor.Blue
            };
      }
}