using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.Areas.Admin.Models.FormModels
{
    public class BlogCollectionFormModel
    {
        public Blog Blog { get; set; }

        public List<BlogTagCategoryCollection> BlogTagCategoryCollections { get; set; }
    }
}
