
using Microsoft.AspNetCore.Mvc;
using Elearning.Models.BLL;
using Elearning.Models.DTO;
using Elearning.Models.Enums;

namespace Elearning.Controllers
{
    
    public class UsersController : BaseController
    {
        [HttpGet]
        public IActionResult Register()
        {
            if (AuthorizedUser == null || AuthorizedUser.PersonType == PersonType.ADMIN)
            {
                return View(new Person());
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Register(Person person)
        {
            if (ModelState.IsValid)
            {
              
                if (PersonsManager.Register(person))
                {
                    return RedirectToAction("Login", "Users");
                }
                else
                {
                    ModelState.AddModelError("", "Emaili ekziston");
                }

            }
            return View(person);
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (!IsAuthorized)
            {
                return View(new PersonLoginModel());
            }
            return this.RedirectToHomePage();
        }
        [HttpPost]
        public IActionResult Login(PersonLoginModel model)
        {
            if (ModelState.IsValid)
            {
                
                var person = PersonsManager.Login(model.Email, model.Password);
                if (person != null)
                {
                    HttpContext.Session.SetString("email", person.Email);
                    HttpContext.Session.SetInt32("id", person.Id);
                    HttpContext.Session.SetInt32("personType", (int)person.PersonType);
                    return this.RedirectToHomePage(person);
                }
                else
                {
                    ModelState.AddModelError("", "Emaili ose passwordi eshte gabim");
                }

            }
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

