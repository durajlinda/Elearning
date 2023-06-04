using System.ComponentModel.DataAnnotations;

namespace Elearning.Models.DTO
{
    public class Professor : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Emri i profesorit duhet vendosur patjeter")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Emri i profesorit duhet te jete midis 5 dhe 20 karakteresh")]
        public string Name { get; set; }


        public string Contact { get; set; }

        public int ProfessorId { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var specialChars = "!@#$%^&*()_+<>?/.,;:'\"[]{}\\|`~";
            if (Name.Any(c => specialChars.Contains(c)))
            {
                yield return new ValidationResult("Emri i profesorit nuk duhet te permbaje karaktere te vecanta", new string[] { nameof(Name) });
            }
        }
    }
}

