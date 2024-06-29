using AutoMapper;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data.Entities;

namespace ProjektCzlowiekKomputer.Profiles
{
    public class MaperProfile : Profile
    {
        public MaperProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookDto,CreateBookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();


            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorDto, CreateAuthorDto>().ReverseMap();
            CreateMap<Author, CreateAuthorDto>().ReverseMap();
            CreateMap<Author, UpdateAuthorDto>().ReverseMap();

            CreateMap<BooksAuthors, BooksAuthorsDto>().ReverseMap();
        }
    }
}
