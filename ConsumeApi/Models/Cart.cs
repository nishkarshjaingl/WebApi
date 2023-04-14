using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumeApi.Models
{
    public class Cart
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        //public virtual UserDetails UserDetails { get; set; }
        

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public virtual Product Products { get; set; }
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity cannot be empty")]
        public int Quantity { get; set; }

        public decimal TotalAmt { get; set; }
        //public virtual ICollection<Product> Products { get; set; }
        
        public virtual UserDetails UserDetails { get; set; }

    }
}
