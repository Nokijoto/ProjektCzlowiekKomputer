using Microsoft.AspNetCore.Identity;
using Project.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Data.Models
{
    public class UserModel: IdentityUser
    {
        public Guid UserGuid { get; set; }

        [MaxLength(30)]
        public string? Name { get; set; }



        public ICollection<UserShelves> UserShelves { get; set; }
    }
    

 }
