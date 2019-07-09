using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AngularMVC.Models;
using AngularMVC.ViewModels;
using Microsoft.AspNet.Identity;


namespace AngularMVC.Controllers.API
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<MovieViewModel> GetMovies()
        {
            var movies = db.Movies.ToList();

            var moviesViewModels = new List<MovieViewModel>();

            foreach (var movie in movies)
            {
                var movieViewModel = new MovieViewModel()
                {
                    ID = movie.Id,
                    Name = movie.Name,
                    Year = movie.YearOfRelease,
                    ProducerID = movie.ProducerId

                };

                var produceractorViewModels = new List<ProducersActorsViewModel>();

                var producersactors = db.ProducersActors.Where(c => c.Id == movie.ProducerId).ToList();

                foreach (var produceractor in producersactors)
                {
                    var produceractorViewModel = new ProducersActorsViewModel()
                    {
                        ID = produceractor.Id,
                        Name = produceractor.Name,
                    };

                    produceractorViewModels.Add(produceractorViewModel);
                }

                movieViewModel.producersactors = produceractorViewModels;

                moviesViewModels.Add(movieViewModel);
            }

            return moviesViewModels;
        }

        [HttpGet]
        [ResponseType(typeof(MovieViewModel))]
        public IHttpActionResult GetMovie(int id)
        {
            var movie = db.Movies.Find(id);

            if (movie == null)
            {
                return NotFound();
            }

            var customerViewModel = new MovieViewModel()
            {
                ID = movie.Id,
                Name = movie.Name,
                Year = movie.YearOfRelease,
                ProducerID = movie.ProducerId
            };

            var produceractorViewModels = new List<ProducersActorsViewModel>();

            var producersactors = db.ProducersActors.Where(c => c.Id == movie.ProducerId).ToList();

            foreach (var produceractor in producersactors)
            {
                var produceractorViewModel = new ProducersActorsViewModel()
                {
                    ID = produceractor.Id,
                    Name = produceractor.Name
                };

                produceractorViewModels.Add(produceractorViewModel);
            }

            customerViewModel.producersactors = produceractorViewModels;

            return Ok(customerViewModel);
        }

        // PUT: api/Movies/5
        [HttpPut]
        [ResponseType(typeof(MovieViewModel))]
        public IHttpActionResult PutMovie(int id, MovieViewModel movieViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movieViewModel.ID)
            {
                return BadRequest();
            }


            try
            {

                var movie = db.Movies.Find(movieViewModel.ID);
                movie.Name = movieViewModel.Name;
                movie.YearOfRelease = movieViewModel.Year;
                movie.ProducerId = movieViewModel.ProducerID;

                db.Entry(movie).State = EntityState.Modified;

                db.SaveChanges();

                foreach (var produceractorViewModel in movieViewModel.producersactors)
                {
                    var produceractor = db.ProducersActors.Where(c => c.Id == movie.ProducerId).Where(n => n.Id == produceractorViewModel.ID).FirstOrDefault();

                    if (produceractor != null)
                    {
                        produceractor.Name = produceractorViewModel.Name;

                        db.Entry(produceractor).State = EntityState.Modified;

                        db.SaveChanges();
                    }
                    else if (produceractor == null)
                    {
                        produceractor = new ProducerActor();

                        produceractor.Id = movie.Id;
                        produceractor.Name = produceractorViewModel.Name;

                        db.ProducersActors.Add(produceractor);

                        db.SaveChanges();
                    }
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        // POST: api/Movies
        [HttpPost]
        [ResponseType(typeof(MovieViewModel))]
        public IHttpActionResult PostMovie(MovieViewModel movieViewModel)
        {

            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            //can not apply modelstate.isvalid because it is looking for ID in model & giving error.

            if (string.IsNullOrEmpty(movieViewModel.Name))
            {
                return BadRequest(ModelState);
            }

            //i should do transactions here to make sure all recrrds insserts correctly
            var movie = new Movie()
            {
                Name = movieViewModel.Name,
                YearOfRelease = movieViewModel.Year,
                ProducerId = movieViewModel.ProducerID
            };

            db.Movies.Add(movie);
            db.SaveChanges();

            //adding customer numbers here
            foreach (var produceractorViewModel in movieViewModel.producersactors)
            {
                var produceractor = new ProducerActor();

                produceractor.Id = movie.Id;
                produceractor.Name = produceractorViewModel.Name;

                db.ProducersActors.Add(produceractor);
                db.SaveChanges();
            }

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieViewModel);
        }

        // DELETE: api/Movies/5
        [HttpDelete]
        [ResponseType(typeof(MovieViewModel))]
        public IHttpActionResult DeleteMovie(int id)
        {
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movie);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoviesExists(int id)
        {
            return db.Movies.Count(e => e.Id == id) > 0;
        }
    }
}
