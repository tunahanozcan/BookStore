using FluentValidation;
using WebApi.Applications.GenreOperations.CreateGenre;

namespace WebApi.Applications.GenreOperations.UpdateGenre
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command=>command.Model.Name).MinimumLength(4).When(x=>x.Model.Name!=string.Empty);
        }
    }
}