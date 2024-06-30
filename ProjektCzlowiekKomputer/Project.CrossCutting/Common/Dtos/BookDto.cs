using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos
{
    public class BookDto:BaseDto
    {
        public string Title { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Publisher { get; set; }
        public int? NumberOfPages { get; set; }
        public string Language { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public string Edition { get; set; }
        public string Format { get; set; }
        public float Rating { get; set; }

        public ICollection<BooksAuthorsDto> BooksAuthors { get; set; }
    }
}
