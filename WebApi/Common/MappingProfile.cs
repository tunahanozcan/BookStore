using AutoMapper;
using WebApi.Applications.BookOperations.GetBookDetail;
using WebApi.Applications.BookOperations.GetBooks;
using WebApi.Applications.GenreOperations.Queries.GetGenreDetail;
using WebApi.Applications.GenreOperations.Queries.GetGenres;
using WebApi.Entities;
using static WebApi.Applications.BookOperations.CreateBook.CreateBookCommand;
using static WebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;

namespace WebApi.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel,Book>();
            CreateMap<Book,BookDetailViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name));
            CreateMap<Book,BooksViewModel>().ForMember(dest=>dest.Genre,opt=>opt.MapFrom(src=>src.Genre.Name)).ForMember(dest=>dest.PublishDate,opt=>opt.MapFrom(src=>src.PublishDate.Date.ToString("dd/MM/yyy")));
            CreateMap<Genre,GenresViewModel>();
            CreateMap<Genre,GenreDetailViewModel>();
            CreateMap<CreateUserModel,User>();
        }
    }
}