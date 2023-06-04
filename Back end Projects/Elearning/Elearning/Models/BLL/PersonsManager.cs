using Elearning.Models.Enums;
namespace Elearning.Models.BLL
{
    public class PersonsManager
    {
        public static bool Register(DTO.Person person)
        {
            var existingPerson = DAL.Person.GetByEmail(person.Email);
            if (existingPerson != null)
            {
                return false;
            }
            return DAL.Person.Insert(new DAL.Person()
            {
                Name = person.Name,
                Surname = person.Surname,
                Email = person.Email,
                Password = person.Password,
                PersonType = PersonType.USER
            });

        }

        public static AuthorizedUser Login(string email, string password)
        {
            var person = DAL.Person.Login(email, password);
            if (person != null)
            {
                return new AuthorizedUser
                {
                    Email = person.Email,
                    Id = person.Id,
                    PersonType = person.PersonType
                };
            }
            return null;
        }

    }
}