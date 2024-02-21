using FluentValidation;
using MovieApiHomeTask.Dtos;

namespace MovieApiHomeTask.Validators;

public class RatingDtoValidator : AbstractValidator<RatingDto>, IRatingDtoValidator
{
    public RatingDtoValidator()
    {
        RuleFor(value => value.MovieId)
            .NotEmpty().WithMessage("Comments cannot be empty.");
        RuleFor(value => value.Rating)
            .NotEmpty().WithMessage("Rating cannot be empty.")
            .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
    }
}