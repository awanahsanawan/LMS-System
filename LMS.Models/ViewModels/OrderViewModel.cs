﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Models.ViewModels
{
    public class OrderViewModel
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<Booking> Booking { get; set; }
    }
}
