using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class MovieRepository : IMovieRepository

    {
        // 注入DbContext
        private readonly MovieShopDbContext _dbContext;
        public MovieRepository(MovieShopDbContext dbContext){
            _dbContext=dbContext;
        }
        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            var movies=await _dbContext.Movies.OrderByDescending(m=>m.Rating).Take(30).ToListAsync();
            return movies;
        }
    }
}
