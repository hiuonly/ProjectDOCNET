using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProjectPRN231API.DTO
{
    public class EUser
    {
        public int userId { get; set; }
        public string uImg { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
        public int roleId { get; set; }
        public string address { get; set; }
    }
}
