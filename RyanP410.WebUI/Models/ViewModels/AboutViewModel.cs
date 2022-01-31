using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Models.ViewModels
{
    public class AboutViewModel
    {
        public Person Person { get; set; }

        public IEnumerable<Service> Services { get; set; }

        public IEnumerable<Pricing> Pricings { get; set; }

        public IEnumerable<FunFact> FunFacts { get; set; }

        public IEnumerable<Client> Clients { get; set; }
    }
}
