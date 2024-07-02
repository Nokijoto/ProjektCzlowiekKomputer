using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos.CreateDto
{
    public class UpdateShelveDto
    {
        public Guid guid;
        public String Name { get; set; }
        public String Description { get; set; }
    }
}
