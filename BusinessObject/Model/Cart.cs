using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cartId { get; set; }
        [ForeignKey("Product")]
        public int productId { get; set; }
        [ForeignKey("User")]
        public int userId { get; set; }
        [Required]
        public int quantity { get; set; }
        [Required]
        public int status { get; set; }
        public virtual Product Products { get; set; }
        public virtual User Users { get; set; }
    }
}
