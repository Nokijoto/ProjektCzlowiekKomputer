using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos
{
    public class BookAuthorsDto:BaseDto
    {
        public int BookId { get; set; }
        public Guid BookGuid { get; set; }
        public BookDto Book { get; set; }
        public int AuthorId { get; set; }
        public Guid AuthorGuid { get; set; }
        public AuthorDto Author { get; set; }
    }
}
