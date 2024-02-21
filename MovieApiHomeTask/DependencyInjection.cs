using MovieApiHomeTask.Clients;
using MovieApiHomeTask.Interfaces;
using MovieApiHomeTask.Repositories;
using MovieApiHomeTask.Services;
using MovieApiHomeTask.Validators;
using Npgsql;
using System.Data;

namespace MovieApiHomeTask;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        //Postgre connection
        var connectionString = configuration.GetConnectionString("PostgreConnection") ?? throw new ArgumentNullException("PostgreSQL connection string was not found.");
        services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));

        services.AddTransient<IMovieRepository, MovieRepository>();
        services.AddTransient<IMovieService, MovieService>();

        //Validator registration
        services.AddTransient<IMovieDtoValidator, MovieDtoValidator>();
        services.AddTransient<IRatingDtoValidator, RatingDtoValidator>();

        //Client registration
        services.AddTransient<CommentClient>();
        services.AddHttpClient();

        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
}
