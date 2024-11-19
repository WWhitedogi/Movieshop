using ApplicationCore.Entities;

namespace ApplicationCore.RepositoryInterface
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetTopRatedMovies();
    }
}
