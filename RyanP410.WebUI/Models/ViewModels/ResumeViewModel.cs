using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Models.ViewModels
{
    public class ResumeViewModel
    {
        public IEnumerable<Experience> Experiences { get; set; }

        public IEnumerable<Education> Educations { get; set; }

        public IEnumerable<Design> Designs { get; set; }

        public IEnumerable<Language> Languages { get; set; }

        public IEnumerable<Coding> Codings { get; set; }

        public IEnumerable<Knowledge> Knowledges { get; set; }
    }
}
