using MovieApiHomeTask.Dtos;
using MovieApiHomeTask.Entities;

namespace MovieApiHomeTask.Interfaces;

public interface IMovieRepository
{
    Task<MovieEntity?> Create(MovieDto movieDto);
    Task<List<MovieEntity>> GetMovieById(int id);
    Task<MovieEntity?> AddRating(RatingDto ratingDto);
    Task<List<RatingDto>> GetAllRatings();
}
