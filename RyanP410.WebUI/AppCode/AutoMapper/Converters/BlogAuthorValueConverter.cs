using AutoMapper;
using RyanP410.WebUI.Models.Entities.Membership;

namespace RyanP410.WebUI.AppCode.AutoMapper.Converters
{
    public class BlogAuthorValueConverter : IValueConverter<RyanUser, string>
    {
        public string Convert(RyanUser member, ResolutionContext context)
        {
            if (member == null)
                return null;

            if (!string.IsNullOrWhiteSpace(member.Name) && !string.IsNullOrWhiteSpace(member.Surname))
            {
                return $"{member.Name} {member.Surname}";
            }
            else if (!string.IsNullOrWhiteSpace(member.Name))
            {
                return member.Name;
            }
            else if (!string.IsNullOrWhiteSpace(member.Surname))
            {
                return member.Surname;
            }
            else if (string.IsNullOrWhiteSpace(member.UserName))
            {
                return member.UserName;
            }

            return member.Email;
        }
    }
}
