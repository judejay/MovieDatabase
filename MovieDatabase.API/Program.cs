using MovieDatabase.Business;
using MovieDatabase.Business.Interfaces;
using MovieDatabase.Data;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Data.Interfaces;
using MovieDatabase.Domain.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]), // Ensure 'Microsoft.EntityFrameworkCore.SqlServer' package is installed
        ServiceLifetime.Scoped);
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();
app.MapGet("api/movies", (IMovieService movieService) =>
{
    return Results.Ok(  movieService.GetAll());
});

app.MapGet("api/movies/{id}", (int id, IMovieService movieService) =>
{
    var movie = movieService.Get(id);
    if (movie == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(movie);
});

app.MapPost("api/movies", (Movie movie, IMovieService movieService) =>
{
    movieService.Create(movie);
    return Results.Created($"/api/movies/{movie.Id}", movie);
});

app.MapDelete("api/movies/{id}", (int id, IMovieService movieService) =>
{
    movieService.Delete(id);
    return Results.NoContent();
});


app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
