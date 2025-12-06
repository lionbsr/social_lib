using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        // 1) Kullanıcıyı ID ile getir
        Task<User?> GetByIdAsync(long id);

        // 2) Kullanıcıyı Username ile getir
        Task<User?> GetByUsernameAsync(string username);

        // 3) Kullanıcıyı Email ile getir
        Task<User?> GetByEmailAsync(string email);

        // 4) Kullanıcı arama (username veya fullName)
        Task<IEnumerable<User>> SearchAsync(string query);

        // 5) Kullanıcı ekleme
        Task AddAsync(User user);
        Task SaveChangesAsync();

        // 6) Kullanıcı güncelleme
        Task UpdateAsync(User user);

        // 7) Takip et
        Task FollowAsync(long followerId, long targetId);

        // 8) Takibi bırak
        Task UnfollowAsync(long followerId, long targetId);

        // 9) Takip ediyor mu kontrol
        Task<bool> IsFollowingAsync(long followerId, long targetId);

        // 10) Takipçi listesi
        Task<IEnumerable<User>> GetFollowersAsync(long userId);

        // 11) Kullanıcının takip ettikleri
        Task<IEnumerable<User>> GetFollowingAsync(long userId);
    }
}
