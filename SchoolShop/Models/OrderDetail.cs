using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Article")]
        public int ArticleId { get; set; }
        [Display(Name = "Artikel")]
        public Article Article { get; set; }

        [Display(Name = "Aantal")]
        public int Count { get; set; }

        [Display(Name = "Prijs")]
        [DisplayFormat(DataFormatString = "€ {0:#,##0.00}", ApplyFormatInEditMode = false)]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal SalesPrice { get; set; }

    }

}
