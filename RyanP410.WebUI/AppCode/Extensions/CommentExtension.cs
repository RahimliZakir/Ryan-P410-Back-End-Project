using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.Extensions
{
    public static partial class Extension
    {
        public static IEnumerable<Comment> GetChildComments(this Comment parent)
        {
            if (parent.ParentId != null)
                yield return parent;

            if (parent.Children != null)
            {
                foreach (Comment child in parent.Children.SelectMany(c => c.GetChildComments()))
                {
                    yield return child;
                }
            }
        }
    }
}
