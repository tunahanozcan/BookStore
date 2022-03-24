using Xunit;
using WebApi.UnitTests;
using WebApi.DBOperations;
using AutoMapper;
using System;
using WebApi.Entities;
using WebApi.Applications.BookOperations.CreateBook;
using static WebApi.Applications.BookOperations.CreateBook.CreateBookCommand;
using FluentAssertions;
using System.Linq;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandsTests :IClassFixture<CommonTestFuture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateBookCommandsTests(CommonTestFuture testFuture)
        {
            _context=testFuture.Context;
            _mapper=testFuture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //Arrange (Hazırlık)
            var book=new Book(){Title="WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",PageCount=100,PublishDate=new DateTime(1990,01,01),GenreId=1};
            _context.Add(book);
            _context.SaveChanges();

            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            command.Model=new CreateBookModel(){Title=book.Title};
            //Act & assert (Çalışma - Doğrulama)
            FluentActions
                .Invoking(()=>command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap zaten mevcut");
        }
        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //Arrange
            CreateBookCommand command=new CreateBookCommand(_context,_mapper);
            CreateBookModel model=new CreateBookModel(){Title="Hobbit",PageCount=1000,PublishDate=DateTime.Now.Date.AddYears(-10),GenreId=1};
            command.Model=model;
            //Act
            FluentActions.Invoking(()=>command.Handle()).Invoke();
            //Assert
            var book=_context.Books.SingleOrDefault(x=>x.Title==model.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.GenreId.Should().Be(model.GenreId);
        }
    }
}