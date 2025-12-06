using Domain.Enums;

namespace Application.Dtos
{
    public class LibraryEntryUpdateRequest
    {
        public long UserId { get; set; }
        public long ContentId { get; set; }

        public LibraryStatus Status { get; set; } // <-- ENUM
    }
}
