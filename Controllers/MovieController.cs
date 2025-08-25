using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieManagementAPI.DTOs.Movie;
using MovieManagementAPI.Models;
using MovieManagementAPI.Services.Interfaces;
using MovieManagementAPI.Utilities;
using System.Threading.Tasks;

namespace MovieManagementAPI.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController(IMovieService movieService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMovies(int pageNumber, int pageSize)
        {
            var result = await movieService.GetMovies(pageNumber, pageSize);
            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(string id)
        {
            var result = await movieService.GetMovie(id);
            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors = 
                ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors).Select(err => err.ErrorMessage).ToList();
                return BadRequest(CustomResult<Movie>.Fail(400, "Could not create movie.", errors));
            }
            var result = await movieService.CreateMovie(request);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(string id, UpdateMovieDTO request)
        {
            if (!ModelState.IsValid)
            {
                var errors =
                ModelState.Where(ms => ms.Value.Errors.Count > 0).SelectMany(kvp => kvp.Value.Errors).Select(err => err.ErrorMessage).ToList();
                return BadRequest(CustomResult<Movie>.Fail(400, "Could not create movie.", errors));
            }
            var result = await movieService.UpdateMovie(id, request);
            if (result.StatusCode == 400)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(string id)
        {
            var result = await movieService.DeleteMovie(id);
            if (result.StatusCode == 404)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
    }
}
