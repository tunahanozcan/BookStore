using FluentValidation;
using WebApi.Applications.GenreOperations.CreateGenre;

namespace WebApi.Applications.GenreOperations.CreateGenre
{
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).NotEmpty().MinimumLength(4);
        }
    }
}