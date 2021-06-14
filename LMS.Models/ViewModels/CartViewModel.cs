using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ViewModels
{
    public class CartViewModel
    {
        public IList<Book> BookList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
