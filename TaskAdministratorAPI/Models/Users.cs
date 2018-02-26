using System.ComponentModel.DataAnnotations;

namespace TaskAdministratorAPI.Models
{
    public class Users
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
