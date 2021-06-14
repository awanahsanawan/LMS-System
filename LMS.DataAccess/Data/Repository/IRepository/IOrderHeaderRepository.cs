using System;
using System.Collections.Generic;
using System.Text;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        IEnumerable<SelectListItem> GetOrderHeaderListForDropDown();

        void Update(OrderHeader orderHeader);

        void ChangeOrderStatus(int orderHeaderId, string status);
    }
}
