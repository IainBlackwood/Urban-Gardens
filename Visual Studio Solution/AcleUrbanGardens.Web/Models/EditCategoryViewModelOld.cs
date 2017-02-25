using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class EditCategoryViewModelOld
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Created By")]
        public virtual string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Create Date")]
        public virtual DateTime CreateDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Updated By")]
        public virtual string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Update Date")]
        public virtual DateTime? UpdateDate { get; set; }

    }
}