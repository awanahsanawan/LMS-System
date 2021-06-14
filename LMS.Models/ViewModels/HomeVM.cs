using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace LMS.Models.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Category> CategoryList { get; set; }
        public IEnumerable<Book> BookList { get; set; }

    }
}
