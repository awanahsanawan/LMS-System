using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LMS.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Student Name")]

        public string Name { get; set; }

        public string Address { get; set; }
        public string Semester { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Date of Birth")]
        public DateTime Dob { get; set; }
        public int Contact { get; set; }
        public ICollection<Booking> Bookings { get; set; }
    }
}
