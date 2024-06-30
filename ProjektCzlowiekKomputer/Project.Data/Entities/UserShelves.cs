using Project.CrossCutting.Common;
using Project.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Entities
{
    public class UserShelves:BaseEntity
    {
        public Guid UserGuid { get; set; }
        public string UserId { get; set; }
        public UserModel User { get; set; }
        public int ShelvesId { get; set; }
        public Guid ShelvesGuid { get; set; }
        public Shelves Shelves { get; set; }
    }
}
