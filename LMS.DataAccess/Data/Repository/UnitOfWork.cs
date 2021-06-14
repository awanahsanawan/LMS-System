using System;
using System.Collections.Generic;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;

namespace LMS.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategroyRepository(_db);
            Author = new AuthorRepository(_db);
            Booking = new BookingRepository(_db);
            Book = new BookRepository(_db);
            Student = new StudentRepository(_db);
            OrderHeader =new OrderHeaderRepository(_db);
            User=new UserRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }

        public IAuthorRepository Author { get; private set; }

        public IBookingRepository Booking { get; private set; }

        public IBookRepository Book { get; private set; }

        public IStudentRepository Student { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IUserRepository User { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
