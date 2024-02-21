using MovieApiHomeTask.Dtos;
using MovieApiHomeTask.Entities;

namespace MovieApiHomeTask.Interfaces;

public interface IMovieService
{
    Task<MovieDto> Create(MovieDto movieDto);
    Task<CommentDto> GetById(int id);
    Task<MovieRatingDto> GetMovieById(int id);
    Task<RatingDto> AddRating(RatingDto ratingDto);
    Task<CommentDto> GetRandomComments(int postId);
    Task<List<RatingDto>> FilterByRating(int ratingFrom, int ratingTo);
}
