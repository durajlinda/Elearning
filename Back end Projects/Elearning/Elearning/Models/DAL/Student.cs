using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Elearning.Models.DAL
{
    public class Student :BaseEntity
    {
        public int Id { get; set; }

       
        public string Name { get; set; }

        public string Contact { get; set; }
        

        public static bool Insert(Student student)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Students (Name, Contact) VALUES (@Name, @Contact)", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Contact", student.Contact);

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
        public static bool Update(Student student)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Students SET Name=@Name, Contact=@Contact WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", student.Name);
                        cmd.Parameters.AddWithValue("@Contact", student.Contact);
                        cmd.Parameters.AddWithValue("@Id", student.Id);
                        con.Open();
                        bool updated = cmd.ExecuteNonQuery() == 1;
                        con.Close();
                        return updated;
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
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Students WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected == 1;
                    }
                }
            }
            catch (Exception ex)
            {
               
                return false;
            }
        }



        public static Student GetStudentById(int id)
        {
            Student result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Students WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            result = new Student
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Contact = (string)reader["Contact"],
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

        public static List<Student> GetStudents()
        {
            List<Student> result = new List<Student>();
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("Select * from Students", con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Student
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Contact = (string)reader["Contact"],
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