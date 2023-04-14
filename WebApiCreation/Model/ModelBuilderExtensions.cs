using Microsoft.EntityFrameworkCore;

namespace WebApiCreation.Model
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>().HasData(
            new UserType() { UserTypeId = 1, UserTypeName = "Admin" },
            new UserType() { UserTypeId = 2, UserTypeName = "Customer" },
            new UserType() { UserTypeId = 3, UserTypeName = "Vendor" }
            );
            modelBuilder.Entity<SecurityQuestion>().HasData(
            new SecurityQuestion() { SqId = 1, Question = "What is your mother's maiden name?" },
            new SecurityQuestion() { SqId = 2, Question = "What is the name of your first pet?" },
            new SecurityQuestion() { SqId = 3, Question = "What was your first car?" },
            new SecurityQuestion() { SqId = 4, Question = "What elementary school did you attend?" },
            new SecurityQuestion() { SqId = 5, Question = "What is the name of the town where you were born?" },
            new SecurityQuestion() { SqId = 6, Question = "When you were young, what did you want to be when you grew up?" },
            new SecurityQuestion() { SqId = 7, Question = "Who was your childhood hero?" },
            new SecurityQuestion() { SqId = 8, Question = "Where was your best family vacation as a kid?" }
            );

            modelBuilder.Entity<Category>().HasData(
            new Category() { CatId = 1, CategoryName = "E-readers" },
            new Category() { CatId = 2, CategoryName = "Books" },
            new Category() { CatId = 3, CategoryName = "Computers" },
            new Category() { CatId = 4, CategoryName = "laptop" },
            new Category() { CatId = 5, CategoryName = "Toys" },
            new Category() { CatId = 6, CategoryName = "Personal Care" },
            new Category() { CatId = 7, CategoryName = "Watches" },
            new Category() { CatId = 8, CategoryName = "Fashion jewellery" },
            new Category() { CatId = 9, CategoryName = "Home" },
            new Category() { CatId = 10, CategoryName = "Kitchen" },
            new Category() { CatId = 11, CategoryName = "Electronic Appliances" },
            new Category() { CatId = 12, CategoryName = "Beauty" },
            new Category() { CatId = 13, CategoryName = "Sports" },
            new Category() { CatId = 14, CategoryName = "Fitness & Outdoors" },
            new Category() { CatId = 15, CategoryName = "Stationery" }
             );


        }
    }
}
