using Elearning.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Elearning.Models.DTO
{


    public class Course : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emri i Kursit duhet vendosur patjeter")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Emri i kursit duhet te jete midis 5 dhe 50 karakteresh")]
        public string Name { get; set; }

        public string Description { get; set; }
        public int ProfessorId { get; set; }

        public Professor Professor { get; set; }

        public Dictionary<string, string> Data { get; set; }
        public bool IsDeleted { get; set; }

        public Course()
        {
            Data = new Dictionary<string, string>();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var specialChars = "!@#$%^&*()_+<>?/.,;:'\"[]{}\\|`~";
            if (Name.Any(c => specialChars.Contains(c)))
            {
                yield return new ValidationResult("Emri nuk duhet te permbaje karaktere te vecanta", new string[] { nameof(Name) });
            }
        }
    }

}