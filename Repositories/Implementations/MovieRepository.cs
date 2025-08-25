using Microsoft.EntityFrameworkCore;
using MovieManagementAPI.Data;
using MovieManagementAPI.DTOs.Movie;
using MovieManagementAPI.Models;
using MovieManagementAPI.Repositories.Interfaces;

namespace MovieManagementAPI.Repositories.Implementations
{
    public class MovieRepository(AppDbContext context) : IMovieRepository
    {
        public async Task<Movie?> CreateMovie(Movie request)
        {
            var movieCreated = await context.Movies.AddAsync(request);
            await context.SaveChangesAsync();
            return movieCreated.Entity;
        }

        public async Task<bool> DeleteMovie(string id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if(movie != null)
            {
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Movie?> GetMovie(string id)
        {
            var movie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            return movie;
        }

        public async Task<List<Movie>?> GetMovies(int pageNumber, int pageSize)
        {
            var movie = await context.Movies.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return movie;
        }

        public async Task<Movie?> UpdateMovie(string id, Movie request)
        {
            var existingMovie = await context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (existingMovie == null) return null;

            context.Entry(existingMovie).CurrentValues.SetValues(request);
            await context.SaveChangesAsync();
            return existingMovie;
        }
    }
}
