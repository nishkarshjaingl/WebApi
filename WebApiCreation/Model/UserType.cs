using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApiCreation.Model
{
    public class UserType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserTypeId { get; set; }

        [DisplayName("User Type")]
        [Required(ErrorMessage = "Login type should not be blank eg. Manager / User")]
        [StringLength(30, MinimumLength = 5)]
        public string UserTypeName { get; set; }

        public virtual ICollection<UserDetails> UserDetails { get; set; }

    }
}
