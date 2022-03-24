using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.BookOperations.CreateBook;
using WebApi.Applications.BookOperations.DeleteBook;
using WebApi.Applications.BookOperations.GetBookDetail;
using WebApi.Applications.BookOperations.GetBooks;
using WebApi.Applications.BookOperations.UpdateBook;
using WebApi.DBOperations;
using static WebApi.Applications.BookOperations.CreateBook.CreateBookCommand;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]s")]
    public class BookController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
    public IActionResult GetBooks()
    {
        GetBooksQuery query=new GetBooksQuery(_context,_mapper);    
        var result=query.Handle();
        return Ok(result);
    }
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        BookDetailViewModel result;
       
            GetBookDetailQuery query=new GetBookDetailQuery(_context,_mapper);
            query.BookId=id;
            GetBookDetailQueryValidator validator=new GetBookDetailQueryValidator();
            validator.ValidateAndThrow(query);
            result=query.Handle();

       return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] CreateBookModel newBook)
    {
        CreateBookCommand commmand=new CreateBookCommand(_context,_mapper);
       
            commmand.Model=newBook;

            CreateBookCommandValidator validator=new CreateBookCommandValidator();
            validator.ValidateAndThrow(commmand);
            commmand.Handle();        
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updatedBook)
    {
        
            UpdateBookCommand command=new UpdateBookCommand(_context);
            command.BookId=id;
            command.Model=updatedBook;
            UpdateBookCommandValidator validator=new UpdateBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
            DeleteBookCommand command=new DeleteBookCommand(_context);
            command.BookId=id;
            DeleteBookCommandValidator validator=new DeleteBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
        
        return Ok();
    }
    }
}