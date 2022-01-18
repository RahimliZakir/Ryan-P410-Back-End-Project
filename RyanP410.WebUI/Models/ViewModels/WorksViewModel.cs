using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Models.ViewModels
{
    public class WorksViewModel
    {
        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<Work> Works { get; set; }
    }
}
