using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LMS.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Booking Date")]
        public DateTime BookingDate { get; set; }
        [Display(Name = "Return Date")]
        public DateTime ReturnDate { get; set; }

        public virtual Student Student { get; set; }
       

        [Required] public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")] public OrderHeader OrderHeader { get; set; }
        [Required] public int BookId { get; set; }
        [ForeignKey("BookId")] public Book Book { get; set; }
        public string BookName { get; set; }
        public int Price { get; set; }

    }
}
