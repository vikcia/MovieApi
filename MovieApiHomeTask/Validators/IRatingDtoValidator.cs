using FluentValidation.Results;
using MovieApiHomeTask.Dtos;

namespace MovieApiHomeTask.Validators;

public interface IRatingDtoValidator
{
    ValidationResult Validate(RatingDto ratingDto);
}