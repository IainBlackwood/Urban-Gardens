using AcleUrbanGardens.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class DeleteCategoryViewModel
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public virtual string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Create Date")]
        public virtual DateTime CreateDate { get; set; }

        [Display(Name = "Updated By")]
        public virtual string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Update Date")]
        public virtual DateTime? UpdateDate { get; set; }

        [Required]
        public virtual string UnassignedCategory { get; set; }

        [Required]
        public virtual int UnassignedCategoryId { get; set; }

        [Required]
        public virtual ICollection<Product> Products { get; set; }
    }
}