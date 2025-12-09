using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Linca_David_ProiectMPA.Data;
using Linca_David_ProiectMPA.Models;

namespace Linca_David_ProiectMPA.Controllers
{
    public class MoviesController : Controller
    {
        private readonly Linca_David_ProiectMPAContext _context;

        public MoviesController(Linca_David_ProiectMPAContext context)
        {
            _context = context;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string sortOrder)
        {
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";

            var movies = _context.Movie
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.Studio)
                .AsNoTracking();

            switch (sortOrder)
            {
                case "title_desc":
                    movies = movies.OrderByDescending(m => m.Title);
                    break;
                case "Year":
                    movies = movies.OrderBy(m => m.ReleaseYear);
                    break;
                case "year_desc":
                    movies = movies.OrderByDescending(m => m.ReleaseYear);
                    break;
                default:
                    movies = movies.OrderBy(m => m.Title);
                    break;
            }

            return View(await movies.ToListAsync());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["DirectorID"] = new SelectList(_context.Director, "ID", "FullName");
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name");
            ViewData["StudioID"] = new SelectList(_context.Studio, "ID", "Name");
            return View();
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Type,ReleaseYear,Synopsis,GenreID,DirectorID,StudioID")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorID"] = new SelectList(_context.Director, "ID", "FullName", movie.DirectorID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", movie.GenreID);
            ViewData["StudioID"] = new SelectList(_context.Studio, "ID", "Name", movie.StudioID);
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            ViewData["DirectorID"] = new SelectList(_context.Director, "ID", "FullName", movie.DirectorID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", movie.GenreID);
            ViewData["StudioID"] = new SelectList(_context.Studio, "ID", "Name", movie.StudioID);
            return View(movie);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Type,ReleaseYear,Synopsis,GenreID,DirectorID,StudioID")] Movie movie)
        {
            if (id != movie.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DirectorID"] = new SelectList(_context.Director, "ID", "FullName", movie.DirectorID);
            ViewData["GenreID"] = new SelectList(_context.Genre, "ID", "Name", movie.GenreID);
            ViewData["StudioID"] = new SelectList(_context.Studio, "ID", "Name", movie.StudioID);
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.Director)
                .Include(m => m.Genre)
                .Include(m => m.Studio)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie != null)
            {
                _context.Movie.Remove(movie);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.ID == id);
        }
    }
}