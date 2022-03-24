using FluentAssertions;
using WebApi.Applications.BookOperations.DeleteBook;
using WebApi.UnitTests;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests:IClassFixture<CommonTestFuture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void WhenBookIdLessThenZeroGiven_Validator_ShouldBeReturnError(int bookId)
        {
            DeleteBookCommand command=new DeleteBookCommand(null);
            command.BookId=bookId;

            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}