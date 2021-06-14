using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.DataAccess.Data.Repository;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Extension;
using LMS.Models;
using LMS.Models.ViewModels;
using LMS.Utility;
using Microsoft.AspNetCore.Authorization;

namespace LMS.Areas.Student.Controllers
{
   
    [Area("Student")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public CartViewModel cartVm { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            cartVm = new CartViewModel()
            {
                OrderHeader = new OrderHeader(),
                BookList = new List<Book>()
            };
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList= new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int bookId in sessionList)
                {
                    cartVm.BookList.Add(_unitOfWork.Book.GetFirstOrDefault(u=>u.Id==bookId,includeProperties:"Category,Author"));
                }
            }
            return View(cartVm);
        }

        public IActionResult Summary()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                foreach (int bookId in sessionList)
                {
                    cartVm.BookList.Add(_unitOfWork.Book.GetFirstOrDefault(u => u.Id == bookId, includeProperties: "Category,Author"));
                }
            }
            return View(cartVm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Summary")]
        public IActionResult SummaryPost()
        {
            if (HttpContext.Session.GetObject<List<int>>(SD.SessionCart) != null)
            {
                List<int> sessionList = new List<int>();
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                cartVm.BookList=new List<Book>();
                foreach (int bookId in sessionList)
                {
                    cartVm.BookList.Add(_unitOfWork.Book.Get(bookId));
                }
            }

            if (!ModelState.IsValid)
            {
                return View(cartVm);
            }
            else
            {
                cartVm.OrderHeader.OrderDate=DateTime.Now;
                cartVm.OrderHeader.Status = SD.StatusSubmitted;
                cartVm.OrderHeader.BookCount = cartVm.BookList.Count;
                _unitOfWork.OrderHeader.Add(cartVm.OrderHeader);
                _unitOfWork.Save();


                foreach (var item in cartVm.BookList)
                {
                    Booking bookingDetails = new Booking()
                    {
                        BookId = item.Id,
                        OrderHeaderId = cartVm.OrderHeader.Id,
                        BookName = item.Title,
                        Price = item.Price
                    };
                    _unitOfWork.Booking.Add(bookingDetails);
                    
                }
                _unitOfWork.Save();
                HttpContext.Session.SetObject(SD.SessionCart,new List<int>());
                return RedirectToAction("OrderConfirmation", "Cart", new {id = cartVm.OrderHeader.Id});
            }
        }

        public IActionResult OrderConfirmation(int id)
        {
            return View(id);
        }

        public IActionResult Remove(int bookId)
        {
            List<int> sessionList = new List<int>();
            sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
            sessionList.Remove(bookId);
            HttpContext.Session.SetObject(SD.SessionCart,sessionList);

            return RedirectToAction(nameof(Index));
        }
    }
}
