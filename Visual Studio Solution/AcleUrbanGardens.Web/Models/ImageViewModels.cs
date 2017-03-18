using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class UpdateImageViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual int? ObjectId { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual string ObjectName { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Controller")]
        public virtual string Controller { get; set; }

        [Required]
        [Display(Name = "Image Path")]
        public virtual string ImagePath { get; set; }
    }
}