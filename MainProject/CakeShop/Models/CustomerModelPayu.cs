using System.ComponentModel.DataAnnotations;

namespace CakeShop.Models
{
    public class CustomerModelPayu
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string ProductInfo { get; set; }
        public string Email { get; set; }
    }
}
