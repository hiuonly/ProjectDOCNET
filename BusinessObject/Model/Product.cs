using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int productId { get; set; }
        [Required]
        public string pImg { get; set; }
        [Required]
        public string productName { get; set; }
        [Required]
        public string productDescription { get; set; }
        [ForeignKey("Brand")]
        public int brandId { get; set; }
        [Required]
        public float price { get; set; }
        [Required]
        public int quantity { get; set; }
        public virtual Brand Brands { get; set; }
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
