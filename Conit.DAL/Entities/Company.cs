using System;

namespace Conit.DAL.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string PictureId { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactName { get; set; }

        public DateTime DateOfAdding { get; set; }

        public string AspNetUserId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
