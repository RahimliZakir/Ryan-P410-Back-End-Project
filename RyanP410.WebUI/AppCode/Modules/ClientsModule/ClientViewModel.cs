namespace RyanP410.WebUI.AppCode.Modules.ClientsModule
{
    public class ClientViewModel
    {
        public int? Id { get; set; }

        public string? ImagePath { get; set; }

        public IFormFile? File { get; set; }

        public string? FileTemp { get; set; }
    }
}
