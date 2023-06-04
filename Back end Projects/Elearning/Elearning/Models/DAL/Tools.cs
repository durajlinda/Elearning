namespace Elearning.Models.DAL
{
    public class Tools
    {
        public static string ConnectionString { get; private set; }

        public static void SetConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}
