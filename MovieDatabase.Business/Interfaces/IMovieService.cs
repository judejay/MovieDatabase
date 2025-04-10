using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabase.Domain.Models;

namespace MovieDatabase.Business.Interfaces
{
    public interface IMovieService
    {
        Movie? Get(int id); 
        IEnumerable<Movie> GetAll();
        void Create(Movie movie);
        void Delete(int id);

    }

}
