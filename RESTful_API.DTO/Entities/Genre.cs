namespace RESTful_API.DTO.Entities
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}