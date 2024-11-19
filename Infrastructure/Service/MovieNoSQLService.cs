// using ApplicationCore.RepositoryInterface;
// using ApplicationCore.ServiceInterface;
// using ApplicationCore.Models;

// namespace Infrastructure.Service
// {
//     // `MovieNoSQLService` 实现了 `IMovieService` 接口
//     public class MovieNoSQLService : IMovieService
//     {
//         // 使用 `readonly` 修饰字段，确保它只能在构造函数中赋值
//         private readonly IMovieRepository _movieRepository;

//         // 构造函数：通过依赖注入传入 `IMovieRepository` 的实例
//         public MovieNoSQLService(IMovieRepository movieRepository)
//         {
//             _movieRepository = movieRepository; // 初始化字段
//         }

//         // 实现接口方法，获取评分最高的电影
//         public IEnumerable<MovieCardResponseModel> GetTopRatedMovies()
//         {
//             // 从 Repository 中获取电影数据
//             var movies = _movieRepository.GetTopRatedMovies();

//             // 将电影数据映射到 `MovieCardResponseModel` 列表
//             var movieCardList = new List<MovieCardResponseModel>();
//             foreach (var movie in movies)
//             {
//                 movieCardList.Add(new MovieCardResponseModel
//                 {
//                     Id = movie.Id,
//                     PosterUrl = movie.PosterUrl
//                 });
//             }

//             // 返回映射后的电影数据
//             return movieCardList;
//         }
//     }
// }
