using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly ApplicationDbContext _db;

        public StudentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetStudentListForDropDown()
        {
            return _db.Student.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Student student)
        {
            var objFromDb = _db.Student.FirstOrDefault(s => s.Id == student.Id);
            objFromDb.Name = student.Name;
            objFromDb.Address = student.Address;
            objFromDb.Semester = student.Semester;
            objFromDb.Gender = student.Gender;
            objFromDb.Dob = student.Dob;
            objFromDb.Contact = student.Contact;


            _db.SaveChanges();
        }
    }
}
