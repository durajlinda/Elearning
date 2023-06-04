using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Elearning.Models.DAL
{
    public class Course : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ProfessorId { get; set; }
        [ForeignKey("ProfessorId")]
        public Professor Professor { get; set; }

        public bool IsDeleted { get; set; }



        public static bool Insert(Course course)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Courses (Name, Description, ProfessorId) VALUES (@Name, @Description, @ProfessorId)", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", course.Name);
                        cmd.Parameters.AddWithValue("@Description", course.Description);
                        cmd.Parameters.AddWithValue("@ProfessorId", course.ProfessorId);
                        con.Open();
                        bool added = cmd.ExecuteNonQuery() == 1;
                        con.Close();
                        return added;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static bool Update(Course course)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Courses SET Name=@Name, Description=@Description, ProfessorId=@ProfessorId WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", course.Name);
                        cmd.Parameters.AddWithValue("@Description", course.Description);
                        cmd.Parameters.AddWithValue("@ProfessorId", course.ProfessorId);
                        cmd.Parameters.AddWithValue("@Id", course.Id);
                        con.Open();
                        bool update = cmd.ExecuteNonQuery() == 1;
                        con.Close();
                        return update;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        public static bool Delete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    con.Open(); 

                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Courses WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected == 1;
                    }

                    con.Close(); 
                }
            }
            catch (Exception ex)
            {
                
            }

            return false;
        }


        public static Course GetCourseById(int id)
        {
            Course result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Courses WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            result = new Course
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"],
                                ProfessorId = (int)reader["ProfessorId"],
                            };
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static List<Course> GetCourses()
        {
            List<Course> result = new List<Course>();
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Courses", con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Course
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Description = (string)reader["Description"],
                                ProfessorId = (int)reader["ProfessorId"]
                            });
                        }
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }


}

