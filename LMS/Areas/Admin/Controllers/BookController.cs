using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;
using LMS.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using JsonResult = Microsoft.AspNetCore.Mvc.JsonResult;

namespace LMS.Areas.Admin.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Area("Admin")]
    public class BookController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        [BindProperty]
        public BookVM BookVm { get; set; }

        public BookController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            BookVm = new BookVM()
            {
                Book = new Models.Book(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                AuthorList = _unitOfWork.Author.GetAuthorListForDropDown(),
            };

            
            if (id != null)
            {
                BookVm.Book = _unitOfWork.Book.Get(id.GetValueOrDefault());
            }

            return View(BookVm);


        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        [Microsoft.AspNetCore.Mvc.ValidateAntiForgeryToken]
        public IActionResult Upsert(BookVM bookVm)
        {

            BookVm = bookVm;
            if (ModelState.IsValid)
            {
                string webRootPath = _hostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (bookVm.Book.Id == 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\books");
                    var extension = Path.GetExtension(files[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(uploads,fileName+extension),FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    bookVm.Book.ImageUrl = @"\images\books\" + fileName + extension;
                    _unitOfWork.Book.Add(bookVm.Book);
                }
                else
                {
                    //edit
                    var bookFromDb = _unitOfWork.Book.Get(bookVm.Book.Id);
                    if (files.Count > 0)
                    {
                        string fileName = Guid.NewGuid().ToString();
                        var uploads = Path.Combine(webRootPath, @"images\books");
                        var extension_new = Path.GetExtension(files[0].FileName);

                        var imagePath = Path.Combine(webRootPath, bookFromDb.ImageUrl.TrimStart('\\'));
                        if(System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension_new), FileMode.Create))
                        {
                            files[0].CopyTo(fileStreams);
                        }

                        bookVm.Book.ImageUrl = @"\images\books\" + fileName + extension_new;
                    }
                    else
                    {
                        bookVm.Book.ImageUrl = bookFromDb.ImageUrl;
                    }
                    _unitOfWork.Book.Update(bookVm.Book);
                }
               _unitOfWork.Save();
               return RedirectToAction(nameof(Index));
            }
            else
            {
                bookVm.CategoryList = _unitOfWork.Category.GetCategoryListForDropDown();
                bookVm.AuthorList = _unitOfWork.Author.GetAuthorListForDropDown();
                return View(bookVm);
            }

           
        }


        #region API CALLS
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public JsonResult GetAll()
        {
            var list = _unitOfWork.Book.GetAll(includeProperties: "Category,Author");
            var data = list.ToList();
           // var data=new { data = _unitOfWork.Book.GetAll(includeProperties:"Category,Author") };
            return Json(data);
        }

        [Microsoft.AspNetCore.Mvc.HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Book.Get(id);
            string webRootPath = _hostEnvironment.WebRootPath;
            var imagePath = Path.Combine(webRootPath, objFromDb.ImageUrl.TrimStart('\\'));
           
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleteing." });
            }
            _unitOfWork.Book.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete successful." });
        }



        #endregion
    }
}
