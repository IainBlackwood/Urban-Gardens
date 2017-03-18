using AcleUrbanGardens.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace AcleUrbanGardens.Web.Models
{
    public class IndexCategoryViewModel
    {
        public virtual IEnumerable<Category> Categories { get; set; }
        public virtual int RowsPerPage { get; set; }
        public virtual Dictionary<string, string> RowOptions { get; set; }

        [Display(Name = "Show Historical Data")]
        public virtual bool? ShowHistoricalData { get; set; }
        public virtual List<ApplicationUser> Users { get; set; }
    }

    public class CreateCategoryViewModel
    {
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

    public class CreateSubCategoryViewModel
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

    public class DeleteAllProductsByCategory
    {
        public IQueryable<Product> products { get; set; }

        public int? CategoryId { get; set; }
    }

    public class DeleteCategoryViewModel
    {
        [Required]
        public virtual Category Category { get; set; }

        [Required]
        public virtual Category Parent { get; set; }

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

        public virtual int ProductRowsPerCategory { get; set; }
        public virtual int SubCategoryRowsPerCategory { get; set; }
        public Dictionary<string, string> RowOptions { get; set; }

    }

    public class DetailsCategoryViewModel
    {
        [Required]
        public virtual Category Category { get; set; }

        public virtual Category Parent { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public virtual string CreatedByUsername { get; set; }

        [Required]
        [Display(Name = "Updated By")]
        public virtual string UpdatedByUsername { get; set; }

        public virtual int ProductRowsPerCategory { get; set; }
        public virtual int SubCategoryRowsPerCategory { get; set; }
        public Dictionary<string, string> RowOptions { get; set; }
    }

    public class EditCategoryViewModel
    {
        [Required]
        [HiddenInput(DisplayValue = false)]
        public virtual int Id { get; set; }

        [Required]
        public virtual string Name { get; set; }

        [Required]
        public virtual string Description { get; set; }

        [Required]
        [Display(Name = "Image")]
        public virtual string ImagePath { get; set; }

        [Required]
        [Display(Name = "Deleted")]
        public virtual bool IsDeleted { get; set; }

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