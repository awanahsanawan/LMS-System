using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Booking booking = new Booking();
            if (id == null)
            {
                return View(booking);
            }

            booking = _unitOfWork.Booking.Get(id.GetValueOrDefault());
            if (booking == null)
            {
                return NotFound();

            }

            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Booking booking)
        {
            if (ModelState.IsValid)
            {
                if (booking.Id == 0)
                {
                    _unitOfWork.Booking.Add(booking);
                }
                else
                {
                    _unitOfWork.Booking.Update(booking);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(booking);
        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Booking.GetAll() });
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
