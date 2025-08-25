using MovieManagementAPI.DTOs.Movie;
using MovieManagementAPI.Models;
using MovieManagementAPI.Utilities;

namespace MovieManagementAPI.Services.Interfaces
{
    public interface IMovieService
    {
        // CREATE
        Task<CustomResult<Movie>> CreateMovie(CreateMovieDTO request);
        // READ ALL
        Task<CustomResult<List<Movie>>> GetMovies(int pageNumber, int pageSize);
        // READ BY ID
        Task<CustomResult<Movie>> GetMovie(string id);
        // UPDATE
        Task<CustomResult<Movie>> UpdateMovie(string id, UpdateMovieDTO request);
        // DELETE
        Task<CustomResult<bool>> DeleteMovie(string id);
    }
}
