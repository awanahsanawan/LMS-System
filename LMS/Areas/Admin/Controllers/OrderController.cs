using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models.ViewModels;
using LMS.Utility;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Details(int id)
        {
            OrderViewModel orderVM = new OrderViewModel()

            {
                OrderHeader = _unitOfWork.OrderHeader.Get(id),
                Booking = _unitOfWork.Booking.GetAll(filter: o => o.OrderHeaderId == id)
            };
            return View(orderVM);
        }

        public IActionResult Approve(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(id,SD.StatusApproved);
            return View(nameof(Index));
        }


        public IActionResult Reject(int id)
        {
            var orderFromDb = _unitOfWork.OrderHeader.Get(id);
            if (orderFromDb == null)
            {
                return NotFound();
            }
            _unitOfWork.OrderHeader.ChangeOrderStatus(id, SD.StatusRejected);
            return View(nameof(Index));
        }


        #region API CALLS
        [HttpGet]
        public JsonResult GetAll()
        {
            var list = _unitOfWork.OrderHeader.GetAll().ToList();
            return Json(new { data = _unitOfWork.OrderHeader.GetAll() });
        }


        public IActionResult GetAllPending()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter:o=>o.Status==SD.StatusPending) });
        }

        public IActionResult GetAllApproved()
        {
            return Json(new { data = _unitOfWork.OrderHeader.GetAll(filter: o => o.Status == SD.StatusApproved) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Booking.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _unitOfWork.Booking.Remove(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful." });
        }



        #endregion
    }
}
