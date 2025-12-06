namespace Application.Dtos
{
    public class ListUpdateRequest
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string? Description { get; set; }
    }
}
