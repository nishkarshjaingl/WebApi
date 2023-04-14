using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApiCreation.Model
{
    public class UserDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [ForeignKey("UserType")]
        [Required(ErrorMessage = "User Type should not be blank")]
        public int UserTypeId { get; set; }

        [DisplayName("FName")]
        [StringLength(30, MinimumLength = 5)]//length
        [Required(ErrorMessage = "First Name can`t be blank")]
        public string FName { get; set; }

        [DisplayName("LName")]
        [StringLength(30, MinimumLength = 3)]//length
        [Required(ErrorMessage = "Last Name can`t be blank")]
        public string LName { get; set; }

        [StringLength(30, MinimumLength = 8)]//length
        [DisplayName("UserName")]
        [Required(ErrorMessage = "User Name can`t be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Date of Birth can`t be blank")]
        public DateTime Dob { get; set; }
        //[StringLength(6, MinimumLength = 4)]//length

        [Required(ErrorMessage = "Please select gender")]
        public string Gender { get; set; }

        [ForeignKey("SecurityQuestion")]
        [Required(ErrorMessage = "Select Security Question")]
        public int SqId { get; set; }

        [DisplayName("Answer")]
        [StringLength(30, MinimumLength = 3)]
        [Required(ErrorMessage = "Security answer cannot be empty")]
        public string SqAns { get; set; }
        public virtual SecurityQuestion SecurityQuestion { get; set; }
        public virtual UserType UserType { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
    }
}
