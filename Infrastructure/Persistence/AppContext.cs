using System;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet'ler (domain entity'lerine göre ekle)
        public DbSet<User> Users => Set<User>();
        public DbSet<Follow> Follows => Set<Follow>();
        public DbSet<Content> Contents => Set<Content>();
        public DbSet<Genre> Genres => Set<Genre>();
        public DbSet<ContentGenre> ContentGenres => Set<ContentGenre>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<PersonRole> PersonRoles => Set<PersonRole>();
        public DbSet<LibraryEntry> LibraryEntries => Set<LibraryEntry>();
        public DbSet<Rating> Ratings => Set<Rating>();
        public DbSet<Review> Reviews => Set<Review>();
        public DbSet<ReviewLike> ReviewLikes => Set<ReviewLike>();
        public DbSet<List> Lists => Set<List>();
        public DbSet<ListItem> ListItems => Set<ListItem>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Book> Books { get; set; }


        // ❗ Çift Follows DbSet kaldırıldı (sende 2 kere vardı, 1 tanesi bırakıldı)

        protected override void OnModelCreating(ModelBuilder b)
        {
            base.OnModelCreating(b);

            // --- users
            b.Entity<User>(e =>
            {
                e.ToTable("users");
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.Email).IsUnique().HasDatabaseName("ux_users_email");
                e.HasIndex(x => x.Username).IsUnique().HasDatabaseName("ux_users_username");
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
                e.Property(x => x.UpdatedAt).HasDefaultValueSql("now()");
            });

            // --- follows composite key (DÜZELTİLMİŞ)
            b.Entity<Follow>(e =>
            {
                e.ToTable("follows");
                e.HasKey(x => new { x.FollowerId, x.FollowedId });

                e.HasOne(x => x.Follower)
                    .WithMany(u => u.Following)
                    .HasForeignKey(x => x.FollowerId)
                    .OnDelete(DeleteBehavior.Restrict); // FIX

                e.HasOne(x => x.Followed)
                    .WithMany(u => u.Followers)
                    .HasForeignKey(x => x.FollowedId)
                    .OnDelete(DeleteBehavior.Restrict); // FIX

                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
                e.HasCheckConstraint("chk_follow_self", "follower_id <> followed_id");
            });

            // --- contents
            b.Entity<Content>(e =>
            {
                e.ToTable("contents");
                e.HasKey(x => x.Id);
                e.HasIndex(x => new { x.ExternalSource, x.ExternalId }).IsUnique().HasDatabaseName("ux_contents_source");
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
                e.Property(x => x.UpdatedAt).HasDefaultValueSql("now()");
                e.HasIndex(x => new { x.Type, x.Year }).HasDatabaseName("ix_contents_type_year");
            });

            // --- genres & content_genres
            b.Entity<Genre>(e =>
            {
                e.ToTable("genres");
                e.HasKey(x => x.Id);
                e.HasIndex(x => new { x.Name, x.Domain }).IsUnique();
            });

            b.Entity<ContentGenre>(e =>
            {
                e.ToTable("content_genres");
                e.HasKey(x => new { x.ContentId, x.GenreId });
                e.HasOne(x => x.Content).WithMany(c => c.ContentGenres).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Genre).WithMany(g => g.ContentGenres).HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.Restrict);
            });

            // --- persons & roles
            b.Entity<Person>(e => { e.ToTable("persons"); e.HasKey(x => x.Id); });

            b.Entity<PersonRole>(e =>
            {
                e.ToTable("person_roles");
                e.HasKey(x => new { x.ContentId, x.PersonId, x.Role });
                e.HasOne(x => x.Content).WithMany(c => c.PersonRoles).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Person).WithMany(p => p.PersonRoles).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
            });

            // --- library_entries
            b.Entity<LibraryEntry>(e =>
            {
                e.ToTable("library_entries");
                e.HasKey(x => new { x.UserId, x.ContentId });
                e.HasOne(x => x.User).WithMany(u => u.LibraryEntries).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Content).WithMany(c => c.LibraryEntries).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
                e.Property(x => x.UpdatedAt).HasDefaultValueSql("now()");
            });

            // --- ratings
            b.Entity<Rating>(e =>
            {
                e.ToTable("ratings");
                e.HasKey(x => new { x.UserId, x.ContentId });
                e.HasOne(x => x.User).WithMany(u => u.Ratings).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Content).WithMany(c => c.Ratings).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
            });

            // --- reviews
            b.Entity<Review>(e =>
            {
                e.ToTable("reviews");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.Reviews).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Content).WithMany(c => c.Reviews).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
                e.HasIndex(x => new { x.ContentId, x.CreatedAt }).HasDatabaseName("ix_reviews_content_created");
                e.HasIndex(x => new { x.UserId, x.CreatedAt }).HasDatabaseName("ix_reviews_user_created");
            });

            // --- review_likes
            b.Entity<ReviewLike>(e =>
            {
                e.ToTable("review_likes");
                e.HasKey(x => new { x.UserId, x.ReviewId });
                e.HasOne(x => x.User).WithMany(u => u.ReviewLikes).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Review).WithMany(r => r.Likes).HasForeignKey(x => x.ReviewId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            });

            // --- lists & list_items
            b.Entity<List>(e =>
            {
                e.ToTable("lists");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.Lists).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
                e.Property(x => x.UpdatedAt).HasDefaultValueSql("now()");
            });

            b.Entity<ListItem>(e =>
            {
                e.ToTable("list_items");
                e.HasKey(x => new { x.ListId, x.ContentId });
                e.HasOne(x => x.List).WithMany(l => l.Items).HasForeignKey(x => x.ListId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Content).WithMany(c => c.ListItems).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.OrderIndex).HasDefaultValue((short)0);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            });

            // --- activities
            b.Entity<Activity>(e =>
            {
                e.ToTable("activities");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.Activities).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.HasOne(x => x.Content).WithMany().HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.Review).WithMany().HasForeignKey(x => x.ReviewId).OnDelete(DeleteBehavior.SetNull);
                e.HasOne(x => x.List).WithMany().HasForeignKey(x => x.ListId).OnDelete(DeleteBehavior.SetNull);
                e.Property(x => x.PayloadJson).HasColumnType("jsonb");
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
                e.HasIndex(x => new { x.UserId, x.CreatedAt }).HasDatabaseName("ix_activities_user_created");
                e.HasIndex(x => x.CreatedAt).HasDatabaseName("ix_activities_created");
            });

            // --- auth tokens
            b.Entity<RefreshToken>(e =>
            {
                e.ToTable("refresh_tokens");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.RefreshTokens).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            });

            b.Entity<PasswordResetToken>(e =>
            {
                e.ToTable("password_reset_tokens");
                e.HasKey(x => x.Id);
                e.HasOne(x => x.User).WithMany(u => u.PasswordResetTokens).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
                e.Property(x => x.CreatedAt).HasDefaultValueSql("now()");
            });
        }
    }
}
