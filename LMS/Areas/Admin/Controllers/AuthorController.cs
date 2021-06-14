using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.DataAccess.Data.Repository.IRepository;
using LMS.Models;

namespace LMS.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Upsert(int? id)
        {
            Author author = new Author();
            if (id == null)
            {
                return View(author);
            }

            author = _unitOfWork.Author.Get(id.GetValueOrDefault());
            if (author == null)
            {
                return NotFound();

            }

            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author author)
        {
            if (ModelState.IsValid)
            {
                if (author.Id == 0)
                {
                    _unitOfWork.Author.Add(author);
                }
                else
                {
                    _unitOfWork.Author.Update(author);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(author);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Author.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.Author.Get(id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _unitOfWork.Author.Remove(objFromDb);
            _unitOfWork.Save();
           
            return Json(new {  success = true, message = "Delete successful." });
        }



        #endregion

    }
}
