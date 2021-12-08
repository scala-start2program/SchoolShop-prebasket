using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [Display(Name = "Merk")]
        [Required(ErrorMessage = "Merknaam is vereist")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimaal 2, maximaal 50 letters")]
        public string BrandName { get; set; }
    }

}
