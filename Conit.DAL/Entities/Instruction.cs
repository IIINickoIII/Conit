﻿using System;

namespace Conit.DAL.Entities
{
    public class Instruction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string Description { get; set; }

        public DateTime DateOfAdding { get; set; }

        public bool IsDeleted { get; set; }
    }
}
