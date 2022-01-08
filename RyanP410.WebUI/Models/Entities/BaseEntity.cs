namespace RyanP410.WebUI.Models.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);
    }
}
