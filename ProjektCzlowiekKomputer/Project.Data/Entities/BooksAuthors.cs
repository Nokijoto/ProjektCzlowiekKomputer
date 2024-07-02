using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class BooksAuthors: BaseEntity
    {
        public int BookId { get; set; }
        public Guid BookGuid { get; set; }
        public Book Book { get; set; }
        public int AuthorId { get; set; }
        public Guid AuthorGuid { get; set; }
        public Author Author { get; set; }

    }
}
