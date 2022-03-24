using System;
using System.Linq;
using FluentAssertions;
using WebApi.Applications.BookOperations.DeleteBook;
using WebApi.DBOperations;
using WebApi.Entities;
using WebApi.UnitTests;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests:IClassFixture<CommonTestFuture>
    {
        private readonly BookStoreDbContext _context;

        public DeleteBookCommandTests(CommonTestFuture testFuture)
        {
            _context = testFuture.Context;
        }

        [Fact]
        public void WhenNotFoundIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
             var book =_context.Books.SingleOrDefault(x=>x.Id==1);
             _context.Remove(book);
             _context.SaveChanges();

            DeleteBookCommand command=new DeleteBookCommand(_context);
            command.BookId=1;
            FluentActions.Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek kitap bulunamadı!");
        }
        [Fact]
        public void WhenFoundIdIsGiven_Book_ShouldBeDelete()
        {
            DeleteBookCommand command=new DeleteBookCommand(_context);
            command.BookId=2;
            FluentActions.Invoking(()=>command.Handle()).Invoke();

            var book=_context.Books.SingleOrDefault(x=>x.Id==2);
            book.Should().BeNull();
        }
    }
}