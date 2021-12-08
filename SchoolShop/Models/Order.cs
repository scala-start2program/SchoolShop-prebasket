using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolShop.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        [Display(Name = "Klant")]
        public User User { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateTimeStamp { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

    }

}
