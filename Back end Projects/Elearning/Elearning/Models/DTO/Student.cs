using Elearning.Models.Enums;
using System.ComponentModel.DataAnnotations;


namespace Elearning.Models.DTO
{

    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Contact { get; set; }

        public bool  IsDeleted {get; set;}

        }


    
}
