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
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null);
    }));

var app = builder.Build();

// Applying migrations
using (var scope = app.Services.CreateScope())
{
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    var retryCount = 0;
    const int maxRetryCount = 5;
    
    while (retryCount < maxRetryCount)
    {
        try
        {
            logger.LogInformation("Tentando aplicar migrations... Tentativa {RetryCount}", retryCount + 1);
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.Migrate();
            logger.LogInformation("Migrations aplicadas com sucesso");
            break;
        }
        catch (Npgsql.NpgsqlException ex)
        {
            retryCount++;
            logger.LogError(ex, "Falha ao aplicar migrations");
            
            if (retryCount >= maxRetryCount)
            {
                logger.LogCritical("Número máximo de tentativas alcançado. Aplicação será encerrada");
                throw;
            }
            
            Thread.Sleep(5000 * retryCount);
        }
    }
}

if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();

// Controller routes
app.UseRouting();
app.MapControllers();

app.Run();
