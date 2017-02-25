using AcleUrbanGardens.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class DeleteCategoryViewModelOld
    {
        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public virtual string UnassignedCategory { get; set; }

        [Required]
        public virtual int UnassignedCategoryId { get; set; }

        [Required]
        public virtual ICollection<Product> Products { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public virtual string CreatedByUsername { get; set; }

        [Required]
        [Display(Name = "Updated By")]
        public virtual string UpdatedByUsername { get; set; }

    }
}