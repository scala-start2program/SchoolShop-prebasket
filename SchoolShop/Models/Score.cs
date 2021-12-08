using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class Score
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [Display(Name = "Gebruiker")]
        public User User { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        [Display(Name = "Artikel")]
        public Article Article { get; set; }

        [Display(Name = "Score")]
        [Range(1, 5, ErrorMessage = "Kies een waarde tussen 1 en 5")]
        public int Stars { get; set; } = 5;

        [Display(Name = "Opmerking")]
        public string Comment { get; set; }
    }
}
