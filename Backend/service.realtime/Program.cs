

// using var channel = GrpcChannel.ForAddress("https://localhost:7243/v1");
// var client = new Greeter.GreeterClient(channel);
// var reply = client.ValidateToken(
//                   new ValidateRequest { Token = "GreeterClient" });


var builder = WebApplication.CreateBuilder(args);

var app = builder
      .ConfigureServices()
      .ConfigurePipeline();

app.Run();
