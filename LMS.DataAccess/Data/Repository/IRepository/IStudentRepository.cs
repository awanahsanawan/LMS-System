using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<SelectListItem> GetStudentListForDropDown();

        void Update(Student student);
    }
}
