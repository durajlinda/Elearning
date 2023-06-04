namespace Elearning.Models.DAL
{

    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; protected set; }
        public bool IsDeleted { get; set; }
        public BaseEntity()
        {
            CreatedOn = DateTime.UtcNow;
        }
    }
}
