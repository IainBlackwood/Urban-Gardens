using AcleUrbanGardens.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class IndexProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Category")]
        public virtual string CategoryName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual int? CategoryId { get; set; }

        [Required]
        public virtual IQueryable<Product> Products { get; set; }

        [Required]
        public virtual IQueryable<Category> Categories { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> CategorySelectList { get; set; }
    }

    public class CreateProductViewModel
    {
        [Required]
        [Display(Name = "Product Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public virtual string LongDescription { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public virtual string ShortDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Created By")]
        public virtual DateTime CreateDate { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Create Date")]
        public virtual string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Updated By")]
        public virtual DateTime? UpdateDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Update Date")]
        public virtual string UpdatedBy { get; set; }

        [Required]
        [Display(Name = "Product Image")]
        public virtual string ProductImage { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual string CategoryName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual int? CategoryId { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Selected Category")]
        public virtual SelectListItem SelectedCategory { get; set; }

        public virtual bool CreatedFromCategoryDetail { get; set; }
    }

    public class EditProductViewModel
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        [Display(Name = "Product Name")]
        public virtual string Name { get; set; }

        [Required]
        [Display(Name = "Long Description")]
        public virtual string LongDescription { get; set; }

        [Required]
        [Display(Name = "Short Description")]
        public virtual string ShortDescription { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Created By")]
        public virtual DateTime CreateDate { get; set; }

        [Required]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Create Date")]
        public virtual string CreatedBy { get; set; }

        [DataType(DataType.DateTime)]
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Updated By")]
        public virtual DateTime? UpdateDate { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Update Date")]
        public virtual string UpdatedBy { get; set; }

        [Required]
        [Display(Name = "Product Image")]
        public virtual string ProductImage { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual string CategoryName { get; set; }

        [HiddenInput(DisplayValue = false)]
        public virtual int? CategoryId { get; set; }

        [Display(Name = "Category")]
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Selected Category")]
        public virtual SelectListItem SelectedCategory { get; set; }

        public virtual bool FromCategoryDetail { get; set; }
    }
}