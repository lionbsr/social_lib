namespace Application.Dtos
{
    public class ListCreateRequest
    {
        public long UserId { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
