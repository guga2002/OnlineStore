using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Persons")]
    public class Person:IdentityUser
    {
        [Column("Name_Of_User")]
        public string? Name { get; set; }

        [Column("Surname_Of_User")]
        public string? Surname { get; set; }

        [Column("Birth_data_Of_user")]
        public DateTime BirthDate { get; set; }
        public Customer? Customer { get; set; }
    }
}
