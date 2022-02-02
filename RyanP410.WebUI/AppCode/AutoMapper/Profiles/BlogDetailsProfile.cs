using AutoMapper;
using RyanP410.WebUI.AppCode.AutoMapper.Converters;
using RyanP410.WebUI.AppCode.Dtos;
using RyanP410.WebUI.Models.Entities;

namespace RyanP410.WebUI.AppCode.AutoMapper.Profiles
{
    public class BlogDetailsProfile : Profile
    {
        public BlogDetailsProfile()
        {
            CreateMap<Blog, BlogDetailsDto>()
                     .ForMember(dest => dest.Collections, src => src.MapFrom(map => map.BlogTagCategoryCollections))
                     .ForMember(dest => dest.ImagePath, src => src.MapFrom(map => $"/uploads/blogs/{map.ImagePath}"))
                     .ForMember(dest => dest.Author, src => src.ConvertUsing(new BlogAuthorValueConverter(), map => map.BlogTagCategoryCollections.FirstOrDefault().CreatedByUser))
                     .ReverseMap();
        }
    }
}
