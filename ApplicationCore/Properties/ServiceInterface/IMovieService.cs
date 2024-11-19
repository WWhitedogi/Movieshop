namespace ApplicationCore.ServiceInterface{
    using ApplicationCore.Models;
    public interface IMovieService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetTopRatedMovies();
    }
}