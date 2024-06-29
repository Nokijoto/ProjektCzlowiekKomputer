using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos.CreateDto
{
    public class UpdateBookDto
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
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
    }
}
