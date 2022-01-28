using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.Models.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string? Path { get; set; }

        public bool IsHttps { get; set; }

        public string QueryString { get; set; }

        public string Method { get; set; }

        public string? Area { get; set; }

        public string? Controller { get; set; }

        public string? Action { get; set; }

        public int StatusCode { get; set; }

        public DateTime RequestTime { get; set; }

        public DateTime ResponseTime { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow.AddHours(4);

        public int? CreatedByUserId { get; set; }

        public virtual RyanUser CreatedByUser { get; set; }
    }
}
