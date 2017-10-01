using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class RateCreateViewModel
    {
        public int LotId { get; set; }

        public decimal RateSize { get; set; }
    }
}