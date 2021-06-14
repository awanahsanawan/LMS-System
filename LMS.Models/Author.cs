using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Author Name")]

        public string Name { get; set; }

        public string Address { get; set; }
        [Display(Name = "Contact Number")]
        public int CellNum { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
