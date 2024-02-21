using FluentValidation;
using MovieApiHomeTask.Dtos;

namespace MovieApiHomeTask.Validators;

public class MovieDtoValidator : AbstractValidator<MovieDto>, IMovieDtoValidator
{
    public MovieDtoValidator()
    {
        RuleFor(value => value.Name)
            .NotEmpty().WithMessage("Name cannot be empty.");
        //RuleFor(value => value.Comments)
        //    .NotEmpty().WithMessage("Comments cannot be empty.");
        //RuleFor(value => value.Rating)
        //    .NotEmpty().WithMessage("Rating cannot be empty.")
        //    .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
    }
}
