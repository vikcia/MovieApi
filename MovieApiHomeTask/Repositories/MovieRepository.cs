using Dapper;
using Microsoft.AspNetCore.Mvc;
using MovieApiHomeTask.Dtos;
using MovieApiHomeTask.Entities;
using MovieApiHomeTask.Interfaces;
using MovieApiHomeTask.Services;
using System.Data;

namespace MovieApiHomeTask.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly IDbConnection _connection;

    public MovieRepository(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<MovieEntity?> Create(MovieDto movieDto)
    {
        string sql = @"
                INSERT INTO movies( 
                    name)
                VALUES( 
                    @Name)
                RETURNING name";

        return await _connection.QuerySingleOrDefaultAsync<MovieEntity>(sql, movieDto);
    }

    public async Task<MovieEntity?> AddRating(RatingDto ratingDto)
    {
        string sql = @"
                INSERT INTO ratings( 
                    movie_id,
                    rating)
                VALUES( 
                    @MovieId,
                    @Rating)
                RETURNING *";

        return await _connection.QuerySingleOrDefaultAsync<MovieEntity>(sql, ratingDto);
    }

    public async Task<List<MovieEntity>> GetMovieById(int id)
    {
        string sql = @"
            SELECT movie_id,
                rating 
            FROM ratings 
            WHERE movie_id = @MovieId";

        var parameters = new { MovieId = id };

        IEnumerable<MovieEntity> result = await _connection.QueryAsync<MovieEntity>(sql, parameters);

        return result.ToList();
    }

    public async Task<List<RatingDto>> GetAllRatings()
    {
        string sql = @"
            SELECT movie_id,
                rating 
            FROM ratings";

        var result = await _connection.QueryAsync<RatingDto>(sql);

        return result.ToList();
    }
}
