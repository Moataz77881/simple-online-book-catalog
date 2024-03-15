using AutoMapper;
using simple_online_book_catalog.models.DomainModel;
using simple_online_book_catalog.models.DTOModel.AuthorDTOs;
using simple_online_book_catalog.models.DTOModel.BookDTO;
using simple_online_book_catalog.models.DTOModel.GenresDTO;
using simple_online_book_catalog.models.DTOModel.ImageDTO;
using simple_online_book_catalog.Models;

namespace simple_online_book_catalog.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Authors,getAuthorDTO>().ReverseMap();
            CreateMap<Authors, createAuthorDTO>().ReverseMap();
            CreateMap<Genres,CreateGenre>().ReverseMap();
            CreateMap<Genres, GetGenresDTO>().ReverseMap();
            CreateMap<Books, CreateBookDTO>().ReverseMap();
            CreateMap<Books, GetBookDTO>()
                //.ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres))
                //.ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors))
                .ReverseMap();
            CreateMap<Books, BookDTO>().ReverseMap();
            CreateMap<Image, ImagesDTO>().ReverseMap();
            CreateMap<Image, responseImageDTO>().ReverseMap();

        }
    }
}
