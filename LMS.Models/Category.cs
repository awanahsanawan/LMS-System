using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class Category
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        [Display (Name ="Category Name")]
       
        public string Name { get; set; }

    }
}
