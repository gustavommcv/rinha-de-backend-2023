using Microsoft.EntityFrameworkCore;
using rinha_de_backend_2023.Context;

var builder = WebApplication.CreateBuilder(args);

// Controllers support
builder.Services.AddControllers();

// Getting connection string
if (builder.Environment.IsDevelopment()) DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
    throw new InvalidOperationException("A variável de ambiente 'DB_CONNECTION_STRING' não foi configurada.");

// Configuring db context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

var app = builder.Build();

// Applying migrations
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try
    {
        db.Database.Migrate();
        Console.WriteLine("Migrações aplicadas com sucesso.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Nenhuma migration aplicada.");
        Console.WriteLine("Motivo: " + ex.Message);
    }
}

if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

// app.UseHttpsRedirection();

// Controller routes
app.UseRouting();
app.MapControllers();

app.Run();
