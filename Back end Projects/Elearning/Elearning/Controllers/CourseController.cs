using Microsoft.AspNetCore.Mvc;
using Elearning.Models.DTO;
using Elearning.Models.BLL;


namespace Elearning.Controllers
{
    public class CourseController : BaseController
    {
        public IActionResult Index()
        {
            List<Course> courses = CourseManager.GetCourses();
            return View(courses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Course course)
        {
            if (ModelState.IsValid)
            {
                CourseManager.AddCourse(course);
                return RedirectToAction("Index");
            }
            return View(course);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Course course = CourseManager.GetCourseById(id);
            if (course != null)
            {
                return View(course);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Course updatedCourse)
        {
            if (ModelState.IsValid)
            {
                CourseManager.UpdateCourse(id, updatedCourse);
                return RedirectToAction(nameof(Index));
            }
            return View(updatedCourse);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {
            Course course = CourseManager.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id, IFormCollection collection)
        {
            bool isDeleted = CourseManager.DeleteCourse(id);
            if (isDeleted)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }



    }
}
