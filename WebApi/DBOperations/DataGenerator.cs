using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context =new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre{
                        Name="Personal Growth"
                    },
                    new Genre{
                        Name="Science Fiction"
                    },
                    new Genre{
                        Name="Romance"
                    }
                );

                context.Books.AddRange(
                new Book{
                    //Id=1,
                    Title="Lean Startup",
                    GenreId=1,
                    PageCount=200,
                    PublishDate=new System.DateTime(2021,12,21)
                },
                new Book{
                    //Id=2,
                    Title="Herland",
                    GenreId=2,
                    PageCount=250,
                    PublishDate=new System.DateTime(2010,05,23)
                },
                new Book{
                    //Id=3,
                    Title="Dune",
                    GenreId=2,
                    PageCount=540,
                    PublishDate=new System.DateTime(2001,06,13)
                });

                context.SaveChanges();
            }
        }
    }
}