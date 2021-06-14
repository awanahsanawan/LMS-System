using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LMS.DataAccess.Data.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetOrderHeaderListForDropDown()
        {
            throw new NotImplementedException();
        }

        public void Update(OrderHeader orderHeader)
        {
            throw new NotImplementedException();
        }

        public void ChangeOrderStatus(int orderHeaderId, string status)
        {
            var orderFromDb = _db.OrderHeader.FirstOrDefault(o => o.Id == orderHeaderId);
            orderFromDb.Status = status;
            _db.SaveChanges();
        }
    }
}
