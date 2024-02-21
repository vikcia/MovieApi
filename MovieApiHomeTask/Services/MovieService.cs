using FluentValidation;
using Microsoft.Extensions.Hosting;
using MovieApiHomeTask.Clients;
using MovieApiHomeTask.Dtos;
using MovieApiHomeTask.Entities;
using MovieApiHomeTask.Interfaces;
using MovieApiHomeTask.Validators;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MovieApiHomeTask.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IMovieDtoValidator _movieDtoValidator;
    private readonly IRatingDtoValidator _ratingDtoValidator;
    private readonly CommentClient _commentClient;

    public MovieService(IMovieRepository movieRepository, IMovieDtoValidator movieDtoValidator, CommentClient commentClient, IRatingDtoValidator ratingDtoValidator)
    {
        _movieRepository = movieRepository;
        _movieDtoValidator = movieDtoValidator;
        _commentClient = commentClient;
        _ratingDtoValidator = ratingDtoValidator;
    }

    public async Task<MovieDto> Create(MovieDto movieDto)
    {
        var result = _movieDtoValidator.Validate(movieDto);

        if (!result.IsValid)
        {
            var validationErrors = string.Join(", ", result.Errors.Select(error => error.ErrorMessage));
            throw new ValidationException(validationErrors);
        }

        MovieEntity movieEntity = await _movieRepository.Create(movieDto) ?? throw new Exception("No data");

        MovieDto movie = new()
        {
            Name = movieEntity.Name
        };

        return movie;
    }

    public async Task<RatingDto> AddRating(RatingDto ratingDto)
    {
        var result = _ratingDtoValidator.Validate(ratingDto);

        if (!result.IsValid)
        {
            var validationErrors = string.Join(", ", result.Errors.Select(error => error.ErrorMessage));
            throw new ValidationException(validationErrors);
        }

        MovieEntity movieEntity = await _movieRepository.AddRating(ratingDto) ?? throw new Exception("No data");

        RatingDto rating = new()
        {
            MovieId = movieEntity.MovieId,
            Rating = movieEntity.Rating
        };

        return rating;
    }

    public async Task<MovieRatingDto> GetMovieById(int id)
    {
        List<MovieEntity> movies = await _movieRepository.GetMovieById(id);

        if (movies.Count == 0)
        {
            throw new Exception("No movie found.");
        }

        CommentDto comment = await GetRandomComments(id) ?? throw new Exception("No comment");

        int rows = movies.Count();
        int totalRating = 0;

        foreach (var movie in movies)
        {
            totalRating += movie.Rating;
        }

        int average = totalRating / rows;

        MovieRatingDto movieRating = new()
        {
            MovieId = movies.First().MovieId,
            Rating = average,
            Comment = comment.Body
        };
        return movieRating;
    }

    public async Task<CommentDto> GetById(int id)
    {
        CommentDto result = await _commentClient.GetById(id);
        if (result is null)
        {
            throw new Exception("Comments not found");
        }

        return result;
    }

    public async Task<CommentDto> GetRandomComments(int postId)
    {
        List<CommentDto> result = await _commentClient.GetRandomComments(postId);
        if (result.Count == 0)
        {
            throw new Exception("No comments found.");
        }
        Random random = new Random();

        var ids = result.Select(p => p.Id).ToArray();

        var minId = ids.Min();
        var maxId = ids.Max();

        int randomIndex = random.Next(minId, maxId);

        var randomComment = result.FirstOrDefault(item => item.Id == randomIndex);

        return randomComment;
    }

    public async Task<List<RatingDto>> FilterByRating(int ratingFrom, int ratingTo)
    {
        List<RatingDto> result = await _movieRepository.GetAllRatings();

        int minRating = ratingFrom;
        int maxRating = ratingTo;

        var averageRatings = result
            .GroupBy(r => r.MovieId)
            .Select(g => new
            {
                MovieId = g.Key,
                Rating = g.Average(r => r.Rating)
            });

        var filteredMovies = averageRatings.Where(movie => movie.Rating >= minRating && movie.Rating <= maxRating);

        List<RatingDto> emptyList = new ();

        foreach (var item in filteredMovies)
        {
            RatingDto filteredRatings = new()
            {
                MovieId = item.MovieId,
                Rating = (int)item.Rating
            };
            emptyList.Add(filteredRatings);
        }

        return emptyList;
    }
}
