using Elearning.Models.DTO;
using System.Collections.Generic;
using System.Linq;

namespace Elearning.Models.BLL
{
    public class StudentManager
    {
        public static List<Student> GetStudents()
        {
            List<DAL.Student> students = DAL.Student.GetStudents();
            List<DTO.Student> dtoStudents = new List<DTO.Student>();
            foreach (var student in students)
            {
                if (!student.IsDeleted) 
                {
                    dtoStudents.Add(new DTO.Student
                    {
                        Id = student.Id,
                        Name = student.Name,
                        Contact = student.Contact
                    });
                }
            }
            return dtoStudents;
        }

        public static DTO.Student GetStudentById(int id)
        {
            DAL.Student student = DAL.Student.GetStudentById(id);
            if (student != null && !student.IsDeleted)
            {
                return new DTO.Student
                {
                    Id = student.Id,
                    Name = student.Name,
                    Contact = student.Contact
                };
            }
            return null;
        }

        public static DTO.Student AddStudent(DTO.Student student)
        {
            DAL.Student dalStudent = new DAL.Student
            {
                Name = student.Name,
                Contact = student.Contact
            };
            bool success = DAL.Student.Insert(dalStudent);
            if (success)
            {
                student.Id = dalStudent.Id;
                return student;
            }
            return null;
        }

        public static bool UpdateStudent(int id, DTO.Student updatedStudent)
        {
            DAL.Student student = DAL.Student.GetStudentById(id);
            if (student != null && !student.IsDeleted)
            {
                student.Name = updatedStudent.Name;
                student.Contact = updatedStudent.Contact;
                return DAL.Student.Update(student);
            }
            return false;
        }


        public static bool DeleteStudent(int id)
        {
            DAL.Student student = DAL.Student.GetStudentById(id);
            if (student != null && !student.IsDeleted)
            {
                student.IsDeleted = true;
                return DAL.Student.Delete(student.Id);
            }
            return false;
        }
    }
}
