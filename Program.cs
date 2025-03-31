var builder = WebApplication.CreateBuilder(args);

// Controllers support
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
}

// app.UseHttpsRedirection();

app.UseRouting();

// Controller routes
app.MapControllers();


app.MapGet("/", () => {
    return "Hello, world";
});

app.Run();
