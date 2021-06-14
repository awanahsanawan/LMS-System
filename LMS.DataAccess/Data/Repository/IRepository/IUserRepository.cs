using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface IUserRepository: IRepository<ApplicationUser>
    {
        void LockUser(string userId);

        void UnLockUser(string userId);
    }
}
