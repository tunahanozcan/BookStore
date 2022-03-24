using Xunit;
using WebApi.UnitTests;
using WebApi.DBOperations;
using AutoMapper;
using System;
using WebApi.Entities;
using WebApi.Applications.BookOperations.CreateBook;
using static WebApi.Applications.BookOperations.CreateBook.CreateBookCommand;
using FluentAssertions;

namespace Application.BookOperations.Commands.CreateBook
{
    public class CreateBookComandValidatorTests :IClassFixture<CommonTestFuture>
    {

        [Theory]
        [InlineData("Lord Of The Rings",0,0)]
        [InlineData("Lord Of The Rings",0,1)]
        [InlineData("Lord Of The Rings",100,0)]
        [InlineData("",0,0)]
        [InlineData("",100,1)]
        [InlineData("",0,1)]
        [InlineData("Lor",100,1)]
        [InlineData("Lord",100,0)]
        [InlineData("Lord",0,1)]
        [InlineData("",100,1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title,int pageCount,int genreId)
        {
            //Arrange
            CreateBookCommand command=new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title=title,
                PageCount=pageCount,
                PublishDate=DateTime.Now.AddDays(-1),
                GenreId=genreId
            };

            //act
            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            var result=validator.Validate(command);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command=new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title="Lord Of The Rings",
                PageCount=100,
                PublishDate=DateTime.Now.Date,
                GenreId=1
            };

            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputGiven_Validator_ShouldNotBeError()
        {
            CreateBookCommand command=new CreateBookCommand(null,null);
            command.Model=new CreateBookModel()
            {
                Title="Lord Of The Rings",
                PageCount=100,
                PublishDate=DateTime.Now.AddYears(-2),
                GenreId=1
            };

            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            var result=validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}