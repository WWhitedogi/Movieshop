using ApplicationCore.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopMVC.Controllers
{
    public class MovieController : Controller
    
    {
        // Dependency injection for service
        // create loosely - couple code
        //_movieService 是存储依赖的字段：保存传入的 IMovieService 实例
        private readonly IMovieService _movieService;

        // Instance of class
        //就是构造IMovieService结构的函数，然后把movieService的实列穿进去
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // GET: /Movie/TopRated
        [HttpGet]
        public async Task<IActionResult> TopRated()
        {
            // Retrieve top-rated movies from the service
            var movies =await _movieService.GetTopRatedMovies();

            // Pass the movie list to the view
            return View(movies);
        }
        //usercontroller
        //genrecontroller

        // GET: /Movie/Details/id
        //public IActionResult Details(int id){
           // var movies = _movieService.GetTopRatedMovies();
           // var movie=movies.Select(m=>m.Id=id).FirstOrDefault();
          //  return View();
        //}
    }
}
