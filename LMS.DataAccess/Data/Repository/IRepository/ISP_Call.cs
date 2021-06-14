using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface ISP_Call:IDisposable
    {
        IEnumerable<T> RetunList<T>(string procedureName);
    }
}
