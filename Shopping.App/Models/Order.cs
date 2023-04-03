
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shopping.App.Models
{
    [Table("Order")]
    // [Index(nameof(No), IsUnique = true)]
    [Index(nameof(UserId))]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        //public long No { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string OrderDate { get; set; }

        [Required]
        public double Price { get; set; }

        public virtual IList<OrderDetail> Details { get; set; }

        [NotMapped]
        public IList<(Product Product, int Quantity)> Products { get; set; }



        [NotMapped]
        public ImageSource DisplayImage
            => Details.First().Product.Image;
        [NotMapped]
        public string DisplayName
            => DisplayBuilder();

        private string DisplayBuilder()
        {
            var first = Details.First();
            var sb = new StringBuilder();
            sb.Append(first.Product.Name);
            sb.Append($" x{first.Quantity}");
            if (Details.Count > 1)
                sb.Append(" ...");

            sb.Append(" Orders");

            return sb.ToString();
        }


        public Order() 
        {
            Details = new List<OrderDetail>();
        }
    }
}
