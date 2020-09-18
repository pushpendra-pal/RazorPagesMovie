using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesMovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Data.RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovie.Data.RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }

        //Search operations
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; } //contains user's search query
        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string MovieGenre { get; set; }

        public async Task OnGetAsync() //requsting the list of all entries
        {
            //Also for search
            IQueryable<string> genreQuery = from m in _context.Movie orderby m.Genre select m.Genre; //the 
            var movies = from m in _context.Movie select m; //LINQ query
            if (!string.IsNullOrEmpty(SearchString))
            {
                movies = movies.Where(s => s.Title.Contains(SearchString)); //the contains method is similar to SQL's LIKE command(case insensitive, in SQLite databases it is case sensitive
            }
            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
