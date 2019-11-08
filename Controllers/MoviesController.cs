using Prac1Proj.Models;
using Prac1Proj.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prac1Proj.Controllers
{
    public class MoviesController : Controller
    {
        private ProjectContext _context;
        public MoviesController()
        {
            _context = new ProjectContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Movies/Random
        public ActionResult Index()
        {
        var movies = _context.Movies.Include(c => c.Genre).ToList();
            //var viewModel = new RandomMovieViewModel()
            //{
            //    Movie = movie,
            //    Customers = customers
            //};
            return View(movies);
            //return Content("Hello World!");
            //return HttpNotFound();
            //return new EmptResult()
            //return RedirectToAction("Index", "Home", new { page = 1, sortBy = "name" });
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
            return View(movie);
        }

        //private IEnumerable<Movie> GetMovies()
        //{
        //    return new List<Movie> {
        //        new Movie { Id = 1,Name = "Shrek"},
        //        new Movie { Id = 2,Name= "Wall-E"}
        //    };
        //}
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = genres
            };
            return View("Moviesform",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _context.Genres.ToList()
                };
                return View("MoviesForm",viewModel);
            }
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var MovieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                MovieInDb.Name = movie.Name;
                MovieInDb.ReleaseDate = movie.ReleaseDate;
                MovieInDb.DateAdded = movie.DateAdded;
                MovieInDb.Genre = movie.Genre;
                MovieInDb.Stock = movie.Stock;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres,
                Id = 0
            };

            return View("MoviesForm",viewModel);
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //        pageIndex = 1;
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //        sortBy = "name";
        //    return Content(String.Format("pageIndex= {0} & SortBy = {1}", pageIndex, sortBy));
        //}
        //[Route("movies/released/{year}/{month}")]
        //public ActionResult ByReleaseDate(int year , int month)
        //{
        //    return Content(year + "/" + month);
        //}
    }
}