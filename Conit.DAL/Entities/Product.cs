namespace Conit.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public string Category { get; set; }

        public string PictureId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
