using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebApi.Applications.GenreOperations.CreateGenre;
using WebApi.Applications.GenreOperations.DeleteGenre;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Applications.GenreOperations.UpdateGenre;
using WebApi.DBOperations;


namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GenreController(IMapper mapper, IBookStoreDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        [HttpGet]
        public ActionResult GetGenres()
        {
            GetGenresQuery query=new GetGenresQuery(_context,_mapper);
            var obj=query.Handle();
            return Ok(obj);
        }
        
        [HttpGet("{id}")]
        public ActionResult GetGenreDetail(int id)
        {
            GetGenreDetailQuery query=new GetGenreDetailQuery(_context,_mapper);
            query.GenreId=id;
            GetGenreDetailQueryValidator validator= new GetGenreDetailQueryValidator();
            validator.ValidateAndThrow(query);

            var obj=query.Handle();
            return Ok(obj);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand command=new CreateGenreCommand(_context);
            command.Model=newGenre;

            CreateGenreCommandValidator validator=new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }

        [HttpPut("id")]
        public IActionResult UpdateGenre(int id,[FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command=new UpdateGenreCommand(_context);
            command.GenreId=id;
            command.Model=updateGenre;

            UpdateGenreCommandValidator valid=new UpdateGenreCommandValidator();
            valid.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
        [HttpDelete("id")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command=new DeleteGenreCommand(_context);
            command.GenreId=id;

            DeleteGenreCommandValidator valid=new DeleteGenreCommandValidator();
            valid.ValidateAndThrow(command);

            command.Handle();
            return Ok();
        }
    }
}