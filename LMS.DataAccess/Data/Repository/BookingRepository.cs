using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;

        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetBookingListForDropDown()
        {
            return _db.Booking.Select(i => new SelectListItem()
            {
                Text = i.BookingDate.ToShortDateString(),
                Value = i.Id.ToString()
            });
        }

        public void Update(Booking booking)
        {
            var objFromDb = _db.Booking.FirstOrDefault(s => s.Id == booking.Id);
            objFromDb.BookingDate = booking.BookingDate;
            objFromDb.ReturnDate = booking.ReturnDate;
            objFromDb.Student.Id = booking.Student.Id;
            objFromDb.Book.Id = booking.Book.Id;

            _db.SaveChanges();
        }
    }
}
