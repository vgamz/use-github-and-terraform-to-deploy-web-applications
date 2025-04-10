using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", async context =>
{
    string value = Environment.GetEnvironmentVariable("region");
    using (var log = new LoggerConfiguration()
    .WriteTo.DatadogLogs("<API_KEY>", 
        configuration: new DatadogConfiguration(){ Url = "https://http-intake.logs.datadoghq.com" },
        tags = new string[] {"region:" + region})
    .CreateLogger())
    {
        // Some code
        await context.Response.WriteAsync("Hello World! This is my $Environment Environment.");
        log.Information("This is my $Environment Environment.");
    }

    
});

app.Run();