using LMS.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Extension;
using LMS.Models.ViewModels;
using LMS.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace LMS.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private HomeVM homeVm;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            homeVm=new HomeVM()
            {
                CategoryList = _unitOfWork.Category.GetAll(),
                BookList = _unitOfWork.Book.GetAll(includeProperties:"Author"),
            };

            return View(homeVm);
        }

        [Authorize(Roles = SD.Student)]

        public IActionResult Order(int? bookId)
        {
            if (bookId != null && bookId > 0)
            {

            }
            var loggedinuser= User.Identity.Name;
            homeVm = new HomeVM()
            {
                CategoryList = _unitOfWork.Category.GetAll(),
                BookList = _unitOfWork.Book.GetAll(includeProperties: "Author"),
            };

            return View(homeVm);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var bookFromDb =_unitOfWork.Book.GetFirstOrDefault(includeProperties: "Category,Author", filter: c => c.Id == id);
            return View(bookFromDb);
        }

        public IActionResult AddToCart(int serviceId)
        {
            List<int> sessionList= new List<int>();
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(SD.SessionCart)))
            {
                sessionList.Add(serviceId);
                HttpContext.Session.SetObject(SD.SessionCart, sessionList);
            }
            else
            {
                sessionList = HttpContext.Session.GetObject<List<int>>(SD.SessionCart);
                if(!sessionList.Contains(serviceId))
                {
                    sessionList.Add(serviceId);
                    HttpContext.Session.SetObject(SD.SessionCart, sessionList);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
