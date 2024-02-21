using FluentValidation.Results;
using MovieApiHomeTask.Dtos;

namespace MovieApiHomeTask.Validators;

public interface IMovieDtoValidator
{
    ValidationResult Validate(MovieDto movieDto);
}
