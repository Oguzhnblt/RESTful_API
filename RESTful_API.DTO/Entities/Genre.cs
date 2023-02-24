namespace RESTful_API.DTO.Entities
{
    public class Genre : BaseEntity
    {
        public string GenreName { get; set; }
        public bool IsActive { get; set; }
    }
}