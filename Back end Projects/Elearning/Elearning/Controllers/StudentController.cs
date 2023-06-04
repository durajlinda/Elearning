using Elearning.Models.BLL;
using Elearning.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;

namespace Elearning.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = StudentManager.GetStudents();
            return View(students);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentManager.AddStudent(student);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Student student = StudentManager.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student updatedStudent)
        {
            if (ModelState.IsValid)
            {
                StudentManager.UpdateStudent(id, updatedStudent);
                return RedirectToAction(nameof(Index));
            }
            return View(updatedStudent);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = StudentManager.GetStudentById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int id)
        {
            bool isDeleted = StudentManager.DeleteStudent(id);
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
