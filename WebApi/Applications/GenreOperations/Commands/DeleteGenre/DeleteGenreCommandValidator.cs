using FluentValidation;

namespace WebApi.Applications.GenreOperations.DeleteGenre
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command=>command.GenreId).GreaterThan(0);
        }
    }
}