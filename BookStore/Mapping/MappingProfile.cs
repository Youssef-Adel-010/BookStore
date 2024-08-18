namespace BookStore.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Categories
        CreateMap<Category, CategoryViewModel>();
        CreateMap<CategoryFormViewModel, Category>().ReverseMap();

        // Authors
        CreateMap<Author, AuthorViewModel>();
        CreateMap<AuthorFormViewModel, Author>().ReverseMap();

    }
}