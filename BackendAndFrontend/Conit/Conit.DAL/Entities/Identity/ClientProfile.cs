﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conit.DAL.Entities.Identity
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
