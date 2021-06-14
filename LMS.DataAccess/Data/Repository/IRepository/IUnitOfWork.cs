using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IAuthorRepository Author { get; }
        IBookingRepository Booking { get; }
        IBookRepository Book { get; }
        IStudentRepository Student { get; }
        IOrderHeaderRepository OrderHeader { get; }
        IUserRepository User { get; }

        void Save();
    }
}
