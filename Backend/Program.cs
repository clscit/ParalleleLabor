using Backend;
using Backend.Controllers;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register services.
        builder.Services.AddTransient<Crud>(provider =>
        {
            return new Crud(GetConnectionString());
        });

        builder.Services.AddTransient<DBController>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    private static string GetConnectionString(string connectionStringName = "Default")
    {
        string output = "";
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
        var config = builder.Build();
        output = config.GetConnectionString(connectionStringName);
        return output;
    }
}