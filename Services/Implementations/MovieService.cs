using MovieManagementAPI.DTOs.Movie;
using MovieManagementAPI.Models;
using MovieManagementAPI.Repositories.Interfaces;
using MovieManagementAPI.Services.Interfaces;
using MovieManagementAPI.Utilities;

namespace MovieManagementAPI.Services.Implementations
{
    public class MovieService(IMovieRepository repository) : IMovieService
    {
        public async Task<CustomResult<Movie>> CreateMovie(CreateMovieDTO request)
        {
            var movieToAdd = new Movie
            {
                Title = request.Title,
                Director = request.Director,
                ReleaseYear = request.ReleaseYear,
                Genre = request.Genre,
                Rating = request.Rating,
            };
            var result = await repository.CreateMovie(movieToAdd);
            if (result == null)
            {
                return CustomResult<Movie>.Fail(400, "Could not create a movie.");
            }
            return CustomResult<Movie>.Ok(result, 201, "Movie created successfully.");
        }

        public async Task<CustomResult<bool>> DeleteMovie(string id)
        {
            var movie = await repository.GetMovie(id);
            if (movie == null)
            {
                return CustomResult<bool>.Fail(404, "Could not get Movie.");
            }
            var result = await repository.DeleteMovie(id);
            return CustomResult<bool>.Ok(result, 200, "Movie deleted successfully.");
        }

        public async Task<CustomResult<Movie>> GetMovie(string id)
        {
            var result = await repository.GetMovie(id);
            if (result == null)
            {
                return CustomResult<Movie>.Fail(404, "Could not get Movie.");
            }
            return CustomResult<Movie>.Ok(result, 200, "Movie fetched successfully.");
        }

        public async Task<CustomResult<List<Movie>>> GetMovies(int pageNumber, int pageSize)
        {
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
            if (pageSize == 0)
            {
                pageSize = 10;
            }
            var result = await repository.GetMovies(pageNumber, pageSize);
            if (result?.Count == 0)
            {
                return CustomResult<List<Movie>>.Fail(404, "Could not get Movies.");
            }
            return CustomResult<List<Movie>>.Ok(result, 200, "Movies fetched successfully.");
        }

        public async Task<CustomResult<Movie>> UpdateMovie(string id, UpdateMovieDTO request)
        {
            if (id != request.Id)
            {
                return CustomResult<Movie>.Fail(400, "Could not update a movie.");
            }
            var existingMovie = await repository.GetMovie(id);
            if (existingMovie == null)
            {
                return CustomResult<Movie>.Fail(404, "Movie not found.");
            }

            existingMovie.Title = request.Title;
            existingMovie.Director = request.Director;
            existingMovie.ReleaseYear = request.ReleaseYear;
            existingMovie.Genre = request.Genre;
            existingMovie.Rating = request.Rating;

            var result = await repository.UpdateMovie(id, existingMovie);
            if (result == null)
            {
                return CustomResult<Movie>.Fail(400, "Could not update a movie.");
            }
            return CustomResult<Movie>.Ok(result, 200, "Movie updated successfully.");
        }
    }
}
