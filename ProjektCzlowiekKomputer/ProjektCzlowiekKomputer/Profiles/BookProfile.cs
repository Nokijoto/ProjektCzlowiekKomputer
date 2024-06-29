using AutoMapper;
using Project.CrossCutting.Dtos;
using Project.CrossCutting.Dtos.CreateDto;
using Project.Data.Entities;

namespace ProjektCzlowiekKomputer.Profiles
{
    public class BookProfile:Profile
    {
        public BookProfile()
        {
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookDto,CreateBookDto>().ReverseMap();
            CreateMap<Book, CreateBookDto>().ReverseMap();
            CreateMap<Book, UpdateBookDto>().ReverseMap();

        }
    }
}
