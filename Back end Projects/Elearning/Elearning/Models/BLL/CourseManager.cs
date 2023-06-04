
using Elearning.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Transactions;


namespace Elearning.Models.BLL
{
    public class CourseManager
    {
        public static List<DTO.Course> GetCourses()
        {
            List<DAL.Course> courses = DAL.Course.GetCourses();
            List<DTO.Course> dtoCourses = new List<DTO.Course>();
            foreach (var course in courses)
            {
                if (course != null && !course.IsDeleted)
                {
                    var professor = DAL.Professor.GetProfessorById(course.ProfessorId);
                    if (professor != null)
                    {
                        dtoCourses.Add(new Course
                        {
                            Id = course.Id,
                            Name = course.Name,
                            Description = course.Description,
                            Professor = new DTO.Professor
                            {
                                Id = professor.Id,
                                Name = professor.Name,
                                Contact = professor.Contact
                            }
                        });
                    }
                }
            }
            return dtoCourses;
        }







        public static DTO.Course GetCourseById(int id)
        {
            DAL.Course course = DAL.Course.GetCourseById(id);
            if (course != null && !course.IsDeleted)
            {
                DAL.Professor professor = DAL.Professor.GetProfessorById(course.ProfessorId);
                if (professor != null)
                {
                    return new DTO.Course
                    {
                        Id = course.Id,
                        Name = course.Name,
                        Description = course.Description,
                        Professor = new DTO.Professor
                        {
                            Id = professor.Id,
                            Name = professor.Name,
                            Contact = professor.Contact
                        }
                    };
                }
                else
                {
                    return new DTO.Course
                    {
                        Id = course.Id,
                        Name = course.Name,
                        Description = course.Description,
                        Professor = null
                    };
                }
            }
            return null;
        }





        public static DTO.Course AddCourse(DTO.Course course)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    DAL.Professor dalProfessor = new DAL.Professor
                    {
                        Name = course.Professor.Name,
                        Contact = course.Professor.Contact
                    };
                    bool professorAdded = DAL.Professor.Insert(dalProfessor);
                    if (!professorAdded)
                    {
                        throw new Exception("Failed to insert Professor.");
                    }

                    DAL.Course dalCourse = new DAL.Course
                    {
                        Name = course.Name,
                        Description = course.Description,
                        ProfessorId = dalProfessor.Id
                    };
                    bool courseAdded = DAL.Course.Insert(dalCourse);
                    if (!courseAdded)
                    {
                        throw new Exception("Failed to insert Course.");
                    }

                    scope.Complete();
                    course.Id = dalCourse.Id;
                    return course;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public static bool UpdateCourse(int id, DTO.Course updatedCourse)
        {
            DAL.Course course = DAL.Course.GetCourseById(id);
            if (course != null && !course.IsDeleted)
            {
                course.Name = updatedCourse.Name;
                course.Description = updatedCourse.Description;
                if (updatedCourse.Professor != null && updatedCourse.Professor.Id != null)
                {
                    DAL.Professor dalProfessor = DAL.Professor.GetProfessorById(updatedCourse.Professor.Id);
                    if (dalProfessor != null)
                    {
                        dalProfessor.Name = updatedCourse.Professor.Name;
                        dalProfessor.Contact = updatedCourse.Professor.Contact;
                        bool professorUpdated = DAL.Professor.Update(dalProfessor);
                        if (!professorUpdated)
                        {
                            throw new Exception("Failed to update Professor.");
                        }

                        course.ProfessorId = dalProfessor.Id;
                    }
                }
                else
                {
                    
                    course.Professor = null;
                }

                return DAL.Course.Update(course);
            }
            return false;
        }





        public static bool DeleteCourse(int id)
        {
            DAL.Course course = DAL.Course.GetCourseById(id);
            if (course != null)
            {
                if (course.ProfessorId != null)
                {
                    _ = DAL.Professor.Delete(course.ProfessorId);
                }

                return DAL.Course.Delete(course.Id);
            }
            return false;
        }








    }

}