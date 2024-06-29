using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos
{
    public class AuthorDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Country { get; set; }
        public string About { get; set; }


        public ICollection<BookAuthorsDto> BooksAuthors { get; set; }
    }
}
