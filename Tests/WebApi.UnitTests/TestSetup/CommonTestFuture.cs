using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TestSetup;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.UnitTests
{
    public class CommonTestFuture
    {
        public BookStoreDbContext Context {get;set;}
        public IMapper Mapper {get; set;}
        public CommonTestFuture()
        {
            var options=new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase(databaseName:"BookStoreTestDb").Options;
            Context=new BookStoreDbContext(options);
            
            Context.Database.EnsureCreated();
            Context.AddBooks();
            Context.AddGenres();
            Context.SaveChanges();

            Mapper=new MapperConfiguration(cfg=>{cfg.AddProfile<MappingProfile>();}).CreateMapper();
        }
    }
}