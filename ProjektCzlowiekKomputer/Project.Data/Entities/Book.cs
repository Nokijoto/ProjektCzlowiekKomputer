using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    [Table("books")]
    public class Book : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(17)]
        [Required]
        public string ISBN { get; set; }

        [MaxLength(100)]
        public string? Genre { get; set; }

        public DateTime? PublicationDate { get; set; }

        [MaxLength(255)]
        public string? Publisher { get; set; }

        public int? NumberOfPages { get; set; }

        [MaxLength(100)]
        public string? Language { get; set; }

        public string? Description { get; set; }

        [MaxLength(255)]
        public string? CoverImage { get; set; }

        [MaxLength(100)]
        public string? Edition { get; set; }

        [MaxLength(100)]
        public string? Format { get; set; }
        public float Rating { get; set; }


        public ICollection<BooksAuthors> BooksAuthors { get; set; }
        public ICollection<BookShelves> BookShelves { get; set; }


    }
}
