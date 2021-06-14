using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        IEnumerable<SelectListItem> GetCategoryListForDropDown();

        void Update(Category category);
    }
}
