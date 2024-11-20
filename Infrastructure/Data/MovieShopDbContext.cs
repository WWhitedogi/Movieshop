using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
        {
        }

        // have all the dbsets as properties with entity types.
        // Migrations
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<MovieCast> MovieCasts { get; set; }
        public DbSet<Cast> Casts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Review> Reviews { get; set; }


        // Fluent API way of modeling the database / creating migrations
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<Trailer>(ConfigureTrailer);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<Cast>(ConfigureCast);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<Purchase>(ConfigurePurchase);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Crew>(ConfigureCrew);
            modelBuilder.Entity<Favorite>(ConfigureFavorite);
            modelBuilder.Entity<Role>(ConfigureRole);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);

        }

        private void ConfigureRole(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Role");
            builder.HasKey(ro => ro.Id);
            builder.Property(ro => ro.Name).HasMaxLength(20);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(ur => new { ur.UserId, ur.RoleId });
            //builder.HasOne(r => r.Role).WithMany(r => r.Users).HasForeignKey(ur => ur.RoleId);
            //builder.HasOne(u => u.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);
        }


        private void ConfigureFavorite(EntityTypeBuilder<Favorite> builder)
        {
            builder.ToTable("Favorite");
            builder.HasKey(f => f.Id);
            builder.HasOne(m => m.Movie).WithMany(m => m.Favorites).HasForeignKey(f => f.MovieId);
            builder.HasOne(u => u.User).WithMany(u => u.Favorites).HasForeignKey(f => f.UserId);
        }

        private void ConfigureCrew(EntityTypeBuilder<Crew> builder)
        {
            builder.ToTable("Crew");
            builder.HasKey(cr => cr.Id);
            builder.Property(cr => cr.Name).HasMaxLength(128);
            builder.Property(cr => cr.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrew");
            builder.HasKey(mcr => new { mcr.MovieId, mcr.CrewId, mcr.Department, mcr.Job });
            builder.HasOne(mcr => mcr.Movie).WithMany(m => m.Crews).HasForeignKey(mcr => mcr.MovieId);
            builder.HasOne(m => m.Crew).WithMany(m => m.Movies).HasForeignKey(mcr => mcr.CrewId);

            builder.Property(mcr => mcr.Department).HasMaxLength(128);
            builder.Property(mcr => mcr.Job).HasMaxLength(128);
        }

        private void ConfigurePurchase(EntityTypeBuilder<Purchase> builder)
        {
            builder.ToTable("Purchase");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.TotalPrice).HasColumnType("decimal(18,2)");
            // builder.Property(p => p.PurchaseNumber).HasColumnType("uniqueidentifier");
            // set the UserId to uniqueidentifier??? => Guid

            builder.HasOne(p => p.User).WithMany(u => u.Purchases).HasForeignKey(p => p.UserId);
            builder.HasOne(p => p.Movie).WithMany(m => m.Purchases).HasForeignKey(p => p.MovieId);
        }


        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(r => new { r.MovieId, r.UserId });
            builder.HasOne(r => r.Movie).WithMany(m => m.Reviews).HasForeignKey(r => r.MovieId);
            builder.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId);

            builder.Property(r => r.Rating).HasColumnType("decimal(3,2)");
        }

        private void ConfigureUser(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(u => u.Id);
            builder.Property(u => u.FirstName).HasMaxLength(128);
            builder.Property(u => u.LastName).HasMaxLength(128);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.HashedPassword).HasMaxLength(1024);
            builder.Property(u => u.Salt).HasMaxLength(1024);
            builder.Property(u => u.PhoneNumber).HasMaxLength(16);

            // Allow Null?? => use ? in Entity
            // builder.Property(u => u.TwoFactorEnabled) => won't work
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCast");
            builder.HasKey(mc => new { mc.MovieId, mc.CastId, mc.Character });
            builder.HasOne(mc => mc.Movie).WithMany(m => m.MovieCasts).HasForeignKey(mc => mc.MovieId);
            builder.HasOne(mc => mc.Cast).WithMany(c => c.MovieCasts).HasForeignKey(mc => mc.CastId);
        }

        private void ConfigureCast(EntityTypeBuilder<Cast> builder)
        {
            builder.ToTable("Cast");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).HasMaxLength(128);
            builder.Property(c => c.ProfilePath).HasMaxLength(2084);
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre"); // when having both data annotation and Fluent API, Fluent API will override data annotation
            builder.HasKey(mg => new { mg.MovieId, mg.GenreId });
            builder.HasOne(mg => mg.Movie).WithMany(g => g.MovieGenres).HasForeignKey(mg => mg.MovieId);
            builder.HasOne(mg => mg.Genre).WithMany(m => m.MovieGenres).HasForeignKey(mg => mg.GenreId);

        }

        private void ConfigureTrailer(EntityTypeBuilder<Trailer> builder)
        {
            builder.ToTable("Trailer");
            builder.HasKey(t => t.Id);
            builder.Property(t => t.TrailerUrl).HasMaxLength(2084);
            builder.Property(t => t.Name).HasMaxLength(2084);
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // here we can model our Movie Table with our rules
            builder.ToTable("Movie"); // = [Table("Movie")]
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Title).HasMaxLength(256); // = [MaxLength(256)]
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m); // set a default value to this column
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

            builder.Ignore(m => m.Rating);
        }

    }
}
