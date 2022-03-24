using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi.Applications.UserOperations.Commands.CreateToken;
using WebApi.Applications.UserOperations.Commands.CreateUser;
using WebApi.Applications.UserOperations.Commands.RefreshToken;
using WebApi.DBOperations;
using WebApi.TokenOperations.Models;
using static WebApi.Applications.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static WebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class UserController:ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        readonly IConfiguration _configuration;
        public UserController(IBookStoreDbContext context,IConfiguration configuration,IMapper mapper)
        {
            _context=context;
            _configuration=configuration;
            _mapper=mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateUserModel newUser)
        {
            CreateUserCommand command=new CreateUserCommand(_context,_mapper);
            command.Model=newUser;
            command.Handle();

            return Ok();
        }

        [HttpPost("connect/token")]
        public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
        {
            CreateTokenCommand command= new CreateTokenCommand(_context,_mapper,_configuration);
            command.Model=login;
            var token=command.Handle();
            return token;
        }
        [HttpGet("RefreshToken")]
        public ActionResult<Token> RefreshToken(string refreshToken)
        {
            RefreshTokenCommand command=new RefreshTokenCommand(_context,_configuration);
            command.RefreshToken=refreshToken;
            var token=command.Handle();
            return token;
        }
    }
}