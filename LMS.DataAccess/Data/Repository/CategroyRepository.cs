using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository
{
    public class CategroyRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategroyRepository(ApplicationDbContext db) : base (db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown()
        {
            return _db.Category.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var objFromDb = _db.Category.FirstOrDefault(s =>s.Id == category.Id);
            objFromDb.Name = category.Name;

            _db.SaveChanges();
        }
    }
}
