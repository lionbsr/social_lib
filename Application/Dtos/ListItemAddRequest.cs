namespace Application.Dtos
{
    public class ListItemAddRequest
    {
        public long ListId { get; set; }
        public long ContentId { get; set; }
        public short OrderIndex { get; set; } = 0;
    }
}
