using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApiCreation.Model
{
    public class SecurityQuestion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SqId { get; set; }

        [DisplayName("Select security question")]
        [Required(ErrorMessage = "Security Question should not be blank")]
        [Column("Question", TypeName = "nvarchar(100)")]
        public string Question { get; set; }
        public virtual ICollection<UserDetails> UserDetails { get; set; }
    }
}
