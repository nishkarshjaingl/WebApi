using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ConsumeApi.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        [ForeignKey("CatId")]
        public int CatId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }

        [DisplayName("ProductName")]
        [StringLength(50, MinimumLength = 5)]
        [Required(ErrorMessage = "Product name cannot be empty")]
        public string ProductName { get; set; }

        public decimal Price { get; set; }

        [DisplayName("Quantity")]
        [Required(ErrorMessage = "Quantity cannot be empty")]
        public int Quantity { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual Category Categories { get; set; }
        public virtual UserDetails UserDetails { get; set; }

    }
}

