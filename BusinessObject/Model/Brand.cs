using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class Brand
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int brandId { get; set; }
        [Required]
        public string brandName { get; set; }
        [Required]  
        public string bImg { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        
    }
}
