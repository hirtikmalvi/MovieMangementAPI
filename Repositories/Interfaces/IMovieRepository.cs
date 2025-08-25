using MovieManagementAPI.DTOs.Movie;
using MovieManagementAPI.Models;

namespace MovieManagementAPI.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        // CREATE
        Task<Movie?> CreateMovie(Movie request);
        // READ ALL
        Task<List<Movie>?> GetMovies(int pageNumber, int pageSize);
        // READ BY ID
        Task<Movie?> GetMovie(string id);
        // UPDATE
        Task<Movie?> UpdateMovie(string id, Movie request);
        // DELETE
        Task<bool> DeleteMovie(string id);
    }
}
