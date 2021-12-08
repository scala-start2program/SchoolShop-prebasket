using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class Article
    {
        public int Id { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        [Display(Name = "Merk")]
        public Brand Brand { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Categorie")]
        public Category Category { get; set; }

        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "€ {0:#,##0.00}", ApplyFormatInEditMode = false)]
        [Range(0.01, 20000, ErrorMessage = "Kies een waarde tussen 1 en 20.000")]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        [Display(Name = "Artikelnaam")]
        [Required(ErrorMessage = "Geef een artikel(naam) op !")]
        [StringLength(250, ErrorMessage = "Modelnaam maximaal 250 letters")]
        public string ArticleName { get; set; }


        [Display(Name = "Omschrijving")]
        [Required(ErrorMessage = "Geef een omschrijving op !")]
        public string Description { get; set; }

        [Display(Name = "Score")]
        [DisplayFormat(DataFormatString = "€ {0:#,##0.00}", ApplyFormatInEditMode = false)]
        [Range(1, 5, ErrorMessage = "Kies een waarde tussen 1 en 5")]
        [Column(TypeName = "decimal(4, 2)")]
        public decimal Score { get; set; }

        public string ImagePath { get; set; }
    }

}
