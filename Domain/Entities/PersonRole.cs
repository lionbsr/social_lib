// Domain/Entities/PersonRole.cs
using Domain.Enums;

namespace Domain.Entities;

public sealed class PersonRole
{
    // Composite PK: ContentId + PersonId + Role
    public long ContentId { get; set; }
    public Content? Content { get; set; }

    public long PersonId { get; set; }
    public Person? Person { get; set; }

    public PersonRoleType Role { get; set; }
}
