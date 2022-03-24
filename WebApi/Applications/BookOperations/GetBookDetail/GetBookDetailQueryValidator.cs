using FluentValidation;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query=>query.BookId).GreaterThan(0);
        }
    }
}