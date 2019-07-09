using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AngularMVC.Models;
using AngularMVC.ViewModels;
using Microsoft.AspNet.Identity;


namespace AngularMVC.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Movies
        public ActionResult Index()
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

                var produceractorName = new List<string>();

                foreach (var produceractor in movie.ProducersActors)
                {
                    produceractorName.Add(produceractor.Name);
                }

                //customerViewModel.NumberDetails = customerNumberDetails;
                //customerViewModel.NumberValues = customerNumberValues;

                moviesViewModels.Add(movieViewModel);
            }

            return View(moviesViewModels);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new MovieViewModel()
            {
                ID = movie.Id,
                Name = movie.Name,
                Year = movie.YearOfRelease,
                ProducerID = movie.ProducerId
            };

            var produceractorName = new List<string>();
            
            foreach (var produceractor in movie.ProducersActors)
            {
                produceractorName.Add(produceractor.Name);
            }

            //customerViewModel.NumberDetails = customerNumberDetails;
            //customerViewModel.NumberValues = customerNumberValues;

            return View(movieViewModel);
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Year,ProducerID")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie();
                {
                    movie.Name = movieViewModel.Name;
                    movie.YearOfRelease = movieViewModel.Year;
                    movie.ProducerId = movieViewModel.ProducerID;
                };

                db.Movies.Add(movie);
                db.SaveChanges();

                //add customer numbers here

                return RedirectToAction("Index");
            }

            return View(movieViewModel);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new MovieViewModel()
            {
                ID = movie.Id,
                Name = movie.Name,
                Year = movie.YearOfRelease,
                ProducerID = movie.ProducerId
            };

            var produceractorName = new List<string>();
           
            foreach (var produceractor in movie.ProducersActors)
            {
                produceractorName.Add(produceractor.Name);
            }

            //customerViewModel.NumberDetails = customerNumberDetails;
            //customerViewModel.NumberValues = customerNumberValues;

            return View(movieViewModel);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Year,ProducerID,Contact")] MovieViewModel movieViewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = db.Movies.Find(movieViewModel.ID);
                movie.Name = movieViewModel.Name;
                movie.YearOfRelease = movieViewModel.Year;
                movie.ProducerId = movieViewModel.ProducerID;

                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(movieViewModel);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var movie = db.Movies.Find(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new MovieViewModel()
            {
                ID = movie.Id,
                Name = movie.Name,
                Year = movie.YearOfRelease,
                ProducerID = movie.ProducerId
            };

            var produceractorName = new List<string>();
    
            foreach (var produceractor in movie.ProducersActors)
            {
                produceractorName.Add(produceractor.Name);
            }

            //customerViewModel.NumberDetails = customerNumberDetails;
            //customerViewModel.NumberValues = customerNumberValues;

            return View(movieViewModel);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var movie = db.Movies.Find(id);

            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}