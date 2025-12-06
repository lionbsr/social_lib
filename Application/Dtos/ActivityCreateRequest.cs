namespace Application.Dtos
{
    public class ActivityCreateRequest
    {
        public long UserId { get; set; }
        public long? ContentId { get; set; }
        public long? ReviewId { get; set; }
        public long? ListId { get; set; }

        // serbest JSON metni
        public string PayloadJson { get; set; } = "{}";
    }
}
