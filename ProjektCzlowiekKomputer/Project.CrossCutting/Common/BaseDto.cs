using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.CrossCutting.Common
{
    public abstract class BaseDto
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
