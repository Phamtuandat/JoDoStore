public static class AppStatusPage
{

      public static void AddStatusCodePage(this IApplicationBuilder app)
      {
            app.UseStatusCodePages(error =>
            {
                  error.Run(async context =>
                  {
                        var response = context.Response;
                        var statusCode = response.StatusCode;
                        var filePath = Directory.GetCurrentDirectory() + "\\Static\\ErrorPage.html";
                        StreamReader str = new StreamReader(filePath);
                        string content = str.ReadToEnd();
                        str.Close();
                        await response.WriteAsync(content.Replace("[StatusCode]", statusCode.ToString()));
                  });
            });
      }
}