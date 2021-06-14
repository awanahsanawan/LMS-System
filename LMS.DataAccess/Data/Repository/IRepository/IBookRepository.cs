using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<SelectListItem> GetBookListForDropDown();

        void Update(Book book);
    }
}
