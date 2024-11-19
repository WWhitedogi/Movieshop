using ApplicationCore.Models;
using ApplicationCore.RepositoryInterface;
using ApplicationCore.ServiceInterface;

namespace Infrastructure.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        //private readonly IAsyncRepository<Movie> _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies()
        {
            // MAPPING => Movie -> MovieCardRepsonseModel
            var movies = await _movieRepository.GetTopRatedMovies();

            var movieCardList = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCardList.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                });
            }

            return movieCardList;
        }
    }
}
