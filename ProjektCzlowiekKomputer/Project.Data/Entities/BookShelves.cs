using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class BookShelves:BaseEntity
    {
        public Guid BookGuid { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ShelvesId { get; set; }
        public Guid ShelvesGuid { get; set; }
        public Shelves Shelves { get; set; }
    }
}
