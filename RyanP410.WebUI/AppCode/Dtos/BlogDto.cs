namespace RyanP410.WebUI.AppCode.Dtos
{
    public class BlogDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public string TagName { get; set; }

        public string BlogCategoryName { get; set; }

        public string? Author { get; set; }
    }
}
