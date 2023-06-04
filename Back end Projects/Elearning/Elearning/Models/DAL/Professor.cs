using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Elearning.Models.DAL
{
    public class Professor : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public int ProfessorId { get; set; }

        
        public Dictionary<string, string> Data { get; set; }

       

        public static bool Insert(Professor professor)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Professors (Name, Contact) VALUES (@Name, @Contact); SELECT SCOPE_IDENTITY()", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", professor.Name);
                        cmd.Parameters.AddWithValue("@Contact", professor.Contact);
                        con.Open();
                        professor.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        con.Close();
                        return professor.Id > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }

        public static bool Update(Professor professor)

        {
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE Professors SET Name=@Name, Contact=@Contact WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", professor.Name);
                        cmd.Parameters.AddWithValue("@Contact", professor.Contact);
                        cmd.Parameters.AddWithValue("@Id", professor.Id);
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
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Professors WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        bool deleted = cmd.ExecuteNonQuery() == 1;
                        con.Close();
                        return deleted;
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            return false;
        }


        public static Professor GetProfessorById(int id)
        {
            Professor result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Professors WHERE Id = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            result = new Professor
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Contact = (string)reader["Contact"]
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


        public static List<Professor> GetProfessors()
        {
            List<Professor> result = new List<Professor>();
            try
            {
                using (SqlConnection con = new SqlConnection(Tools.ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Professors", con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            result.Add(new Professor
                            {
                                Id = (int)reader["Id"],
                                Name = (string)reader["Name"],
                                Contact = (string)reader["Contact"]
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
