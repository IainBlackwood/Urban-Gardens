using AcleUrbanGardens.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class CreateSubCategoryViewModelOld
    {
        public virtual int Id { get; set; }

        // to enable sub-categories
        [Required]
        //[HiddenInput(DisplayValue = false)]
        public int? ParentId { get; set; }

        //[HiddenInput(DisplayValue = false)]
        public virtual Category Parent { get; set; }
        //public virtual ICollection<Category> Children { get; set; }

        //// a category contains many products
        //public virtual ICollection<Product> Products { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual string CreatedBy { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime CreateDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual string UpdatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        public virtual DateTime? UpdateDate { get; set; }
    }
}