using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using System.ComponentModel.DataAnnotations;   

namespace Infrastructure.Data
{
    public class MovieShopDbContext : DbContext
    {
        public DbSet<Movie> Movies{get;set;}
        public DbSet<Genre> Genres{get;set;}
        public DbSet<MovieGenre> MovieGenres{get;set;}
        public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //配置MovieGenre中间表
            modelBuilder.Entity<MovieGenre>(builder=>{
                builder.ToTable("MovieGenre");
                builder.HasKey(mg=>new{mg.MovieId,mg.GenreId});
                builder.HasOne(mg=>mg.Movie)
                        .WithMany(m=>m.MovieGenre)
                        .HasForeignKey(mg=>mg.MovieId);

                builder.HasOne(mg=>mg.Genre)
                        .WithMany(g=>g.MovieGenre)
                        .HasForeignKey(mg=>mg.GenreId);

            });

            //配置Movie表
            modelBuilder.Entity<Movie>(builder=>{
                builder.ToTable("Movie");
                builder.HasKey(modelBuilder=>modelBuilder.Id);
                builder.Property(m=>m.Title).HasMaxLength(256);
            });

            
        }
    }
}
