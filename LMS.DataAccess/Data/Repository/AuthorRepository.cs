using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetAuthorListForDropDown()
        {
            return _db.Author.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Author author)
        {
            var objFromDb = _db.Author.FirstOrDefault(s => s.Id == author.Id);
            objFromDb.Name = author.Name;
            objFromDb.Address = author.Address;
            objFromDb.CellNum = author.CellNum;


            _db.SaveChanges();
        }
    }
}
