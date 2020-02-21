using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queries
{
    public class Program
    {
        static void Main(string[] args)
        {
            var movies = new List<Movie>
            {
                new Movie {Title = "Lord of the Rings", Rating = 10.0f, Year = 2003},
                new Movie {Title = "Forrest Gump", Rating = 9.2f, Year = 1996},
                new Movie {Title = "Duck Soup", Rating = 8.8f, Year = 1938},
                new Movie {Title = "The Hobbit", Rating = 9.4f, Year = 2008}
            };

            var query = movies.Where(m => m.Year > 2000).ToList();

            Console.WriteLine(query.Count());
            var enumerator = query.GetEnumerator();
            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }

            foreach (var movie in query)
            {
                Console.WriteLine(movie.Title);
            }
        }
    }
}
