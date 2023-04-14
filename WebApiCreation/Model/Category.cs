using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApiCreation.Model
{
    public class Category
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatId { get; set; }

        [DisplayName("Category Name")]
        [Required(ErrorMessage = "Category Name should not be blank")]
        [StringLength(30, MinimumLength = 5)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

}

