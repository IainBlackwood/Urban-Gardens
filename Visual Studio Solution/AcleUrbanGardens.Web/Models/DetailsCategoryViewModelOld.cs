using AcleUrbanGardens.Domain;
using System.ComponentModel.DataAnnotations;

namespace AcleUrbanGardens.Web.Models
{
    public class DetailsCategoryViewModelOld
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
    }
}