using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabase.Business.Interfaces;
using MovieDatabase.Data.Interfaces;
using MovieDatabase.Domain.Models;

namespace MovieDatabase.Business
{
   
        public class MovieService : IMovieService
        {
            private readonly IRepository<Movie> repository;

            public MovieService(IRepository<Movie> repository)
            {
                this.repository = repository;
            }

            public void Create(Movie movie)
            {
                repository.Create(movie);
            }

            public void Delete(int id)
            {
                Movie? toDelete = Get(id) ?? throw new Exception("Movie not found.");

                repository.Delete(toDelete);
            }

            public Movie? Get(int id)
            {
                return repository.GetAll().SingleOrDefault(x => x.Id == id);
            }

            public IEnumerable<Movie> GetAll()
            {
                return repository.GetAll().ToList();
            }
        }

}
