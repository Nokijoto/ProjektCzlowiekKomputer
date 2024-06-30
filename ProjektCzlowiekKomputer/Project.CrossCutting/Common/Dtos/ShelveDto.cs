using Project.CrossCutting.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Dtos
{
    public class ShelveDto:BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
