using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Categories
        CreateMap<Category, CategoryViewModel>();
        CreateMap<CategoryFormViewModel, Category>().ReverseMap();
        CreateMap<Category, SelectListItem>()
            .ForMember(
                dest => dest.Value,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Text,
                opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        // Authors
        CreateMap<Author, AuthorViewModel>();
        CreateMap<AuthorFormViewModel, Author>().ReverseMap();
        CreateMap<Author, SelectListItem>()
            .ForMember(
                dest => dest.Value,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.Text,
                opt => opt.MapFrom(src => src.Name))
            .ReverseMap();

        // Books
        CreateMap<Book, BookFormViewModel>()
            .ForMember(
                dest => dest.Categories,
                opt => opt.Ignore())
            .ReverseMap();

    }
}