using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos
{
    public class BookShelvesDto :BaseDto
    {
        public Guid BookGuid { get; set; }
        public int BookId { get; set; }
        public BookDto Book { get; set; }
        public int ShelvesId { get; set; }
        public Guid ShelvesGuid { get; set; }
        public ShelveDto Shelves { get; set; }
    }
}
