using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterface;
using Infrastructure.Data;

namespace Infrastructure.Repository
{
    public class MovieRepository : IMovieRepository
    {
        // DI 
        private readonly MovieShopDbContext _dbContext;
        private readonly IAsyncRepository<Movie> _efRepository;

        public MovieRepository(MovieShopDbContext dbContext, IAsyncRepository<Movie> efRepository)
        {
            _dbContext = dbContext;
            _efRepository = efRepository;   
        }

        public async Task<IEnumerable<Movie>> GetTopRatedMovies()
        {
            // DB retrieve data -> LINQ 
            //var movies = await _dbContext.Movies.Select(m => new Movie
            //{
            //    Id = m.Id,s
            //    PosterUrl = m.PosterUrl,
            //    Title = m.Title,
            //    ReleaseDate = m.ReleaseDate,
            //    Rating = m.Rating,
            //}).Take(30).ToArrayAsync();

            var movies = await _efRepository.ListAllAsync();

            return movies;
        }
    }
}
