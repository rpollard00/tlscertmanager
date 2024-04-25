using System.Text.Json.Serialization;
using Adapter.Api.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
public class AdapterApi
{
    private WebApplicationBuilder _builder;

    public AdapterApi(string[] args, Action<IServiceCollection> options)
    {
        _builder = WebApplication.CreateBuilder(args);

        options.Invoke(_builder.Services);

        System.Console.WriteLine("Constructor invoked");
        _builder.Services.AddEndpointsApiExplorer();
        _builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });
        _builder.Services.AddSwaggerGen();

        // Necessary when called from a different assembly
        var assembly = typeof(HelloWorldController).Assembly;

        _builder.Services.AddControllers()
                .PartManager.ApplicationParts.Add(new AssemblyPart(assembly));
    }

    public Task RunAsync()
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        var app = _builder.Build();
        // Output registered controllers and routes
        System.Console.WriteLine("Run async executing");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hello"));
        }
        // app.UseHttpsRedirection();


        // app.UseAuthorization();
        app.MapControllers();
        return app.RunAsync();
    }


}


//
// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", () =>
// {
//     var forecast = Enumerable.Range(1, 5).Select(index =>
//         new WeatherForecast
//         (
//             DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//             Random.Shared.Next(-20, 55),
//             summaries[Random.Shared.Next(summaries.Length)]
//         ))
//         .ToArray();
//     return forecast;
// })
// .WithName("GetWeat_herForecast")
// .WithOpenApi();
//
// app.Run();
//
// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }
