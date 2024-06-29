using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class Author:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public string? Country { get; set; }
        public string? About { get; set; }

       
        public ICollection<BooksAuthors> BooksAuthors { get; set; }
    }
}
