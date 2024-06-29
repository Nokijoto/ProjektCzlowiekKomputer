using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class Shelves:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<BookShelves> BookShelves { get; set; }
    }
}
