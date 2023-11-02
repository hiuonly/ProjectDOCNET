using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN231API.DTO
{
    public class ACart
    {
        public int productId { get; set; }
        public int userId { get; set; }
        public int quantity { get; set; }
    }
}
