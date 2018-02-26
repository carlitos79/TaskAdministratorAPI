using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskAdministratorAPI.Models
{
    public class Tasks
    {
        [Key]
        public int TaskID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Begin")]
        [DataType(DataType.Text)]
        public DateTime BeginDateTime { get; set; }

        [Required]
        [Display(Name = "Deadline")]
        [DataType(DataType.Text)]
        public DateTime DeadlineDateTime { get; set; }

        [Required]
        public string Requirements { get; set; }

        [Display(Name = "Responsable(s)")]
        [DataType(DataType.Text)]
        public List<Users> Responsables { get; set; }
    }
}
