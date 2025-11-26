// Domain/Entities/Person.cs
namespace Domain.Entities;

public sealed class Person
{
    public long Id { get; set; }
    public string Name { get; set; } = null!;
    public string? AvatarUrl { get; set; }

    public ICollection<PersonRole> PersonRoles { get; set; } = new List<PersonRole>();
}
