using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.Model
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userId { get; set; }
        [Required]
        public string uImg { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [ForeignKey("Role")]
        public int roleId { get; set; }
        [Required]
        public  string address { get; set; }
        public virtual Role Roles { get; set; }
        public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
    }
}
