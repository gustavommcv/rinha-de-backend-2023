using Microsoft.EntityFrameworkCore;
using rinha_de_backend_2023.Context;
using rinha_de_backend_2023.Contracts;
using rinha_de_backend_2023.Repositories;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll", policy => {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Controllers support
builder.Services.AddControllers();

var port = Environment.GetEnvironmentVariable("APP_PORT") ?? "8080";

// Getting connection string
// if (builder.Environment.IsDevelopment()) DotNetEnv.Env.Load();
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ??
    throw new InvalidOperationException("A variável de ambiente 'DB_CONNECTION_STRING' não foi configurada.");

// Configuring db context
builder.Services.AddDbContextPool<AppDbContext>(options => {
    options.UseNpgsql(connectionString);
}); // Can configure the pool size

// Repository registered
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

var app = builder.Build();

// CORS
app.UseCors("AllowAll");

// Applying migrations
using (var scope = app.Services.CreateScope()) {
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    try {
        db.Database.Migrate();
        Console.WriteLine("Migrações aplicadas com sucesso.");
    }
    catch (Exception ex) {
        Console.WriteLine("Nenhuma migration aplicada.");
        Console.WriteLine("Motivo: " + ex.Message);
    }
}

// if (app.Environment.IsDevelopment()) { app.UseDeveloperExceptionPage(); }

// app.UseHttpsRedirection();

// Controller routes
app.UseRouting();
app.MapControllers();

app.Run($"http://*:{port}");
