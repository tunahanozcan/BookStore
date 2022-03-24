using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApi.Common;
using WebApi.DBOperations;

namespace WebApi.Applications.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _dbContext;
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }

        public UpdateBookCommand(IBookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Handle()
        {
        var book=_dbContext.Books.FirstOrDefault(x=>x.Id==BookId);

        if(book is null)
            throw new InvalidOperationException("Güncellenecek kitap bulunamadı!");
        
        book.GenreId=Model.GenreId != default ? Model.GenreId :book.GenreId;
        book.Title=Model.Title!=default?Model.Title:book.Title;
        
        _dbContext.SaveChanges();
        }
    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
    }
}