using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetBookListForDropDown()
        {
            return _db.Book.Select(i => new SelectListItem()
            {
                Text = i.Title,
                Value = i.Id.ToString()
            });
        }

        public void Update(Book book)
        {
            var objFromDb = _db.Book.FirstOrDefault(s => s.Id == book.Id);
            objFromDb.Title = book.Title;
            objFromDb.Price = book.Price;
            objFromDb.Quantity = book.Quantity;
            objFromDb.Edition = book.Edition;
            //objFromDb.Author = book.Author;
            //objFromDb.ImageUrl = book.ImageUrl;
            //objFromDb.Categroy.Id = book.Categroy.Id;

            _db.SaveChanges();
        }
    }
}
