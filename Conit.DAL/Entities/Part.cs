using System;

namespace Conit.DAL.Entities
{
    public class Part
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Color { get; set; }

        public string Material { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public int Length { get; set; }

        public DateTime DateOfAdding { get; set; }

        public int PictureId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
