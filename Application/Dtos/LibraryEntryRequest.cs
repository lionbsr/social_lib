using Domain.Enums;

namespace Application.Dtos
{
    public class LibraryEntryRequest
    {
        public long UserId { get; set; }
        public long ContentId { get; set; }

        public LibraryStatus Status { get; set; }  // <-- ENUM
    }
}
