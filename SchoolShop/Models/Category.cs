using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name = "Categorie")]
        [Required(ErrorMessage = "Categorienaam is vereist")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Minimaal 2, maximaal 50 letters")]
        public string CategoryName { get; set; }
        public string ImagePath { get; set; }
    }

}
