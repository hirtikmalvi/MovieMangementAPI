using Microsoft.OpenApi.Services;
using Moq;
using MovieManagementAPI.DTOs.Movie;
using MovieManagementAPI.Models;
using MovieManagementAPI.Repositories.Interfaces;
using MovieManagementAPI.Services.Implementations;
using MovieManagementAPI.Services.Interfaces;
using MovieManagementAPI.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagementAPI.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _repo;
        private readonly MovieService movieService;

        public MovieServiceTests()
        {
            _repo = new Mock<IMovieRepository>();
            movieService = new MovieService(_repo.Object);
        }

        //
        // CREATE: CreateMovie(CreateMovieDTO)
        //

        [Fact]
        public async Task CreateMovie_ShouldReturn201AndMovie_WhenRepositoryCreates()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var dto = new CreateMovieDTO { Title = "Inception", Director = "Nolan", ReleaseYear = 2010, Genre = "Sci-Fi", Rating = 9 };

            var created = new Movie
            {
                Id = "1",
                Title = dto.Title,
                Director = dto.Director,
                ReleaseYear = dto.ReleaseYear,
                Genre = dto.Genre,
                Rating = dto.Rating
            };

            _repo.Setup(r => r.CreateMovie(It.IsAny<Movie>())).ReturnsAsync(created);

            // Act
            var result = await movieService.CreateMovie(dto);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(created, result.Data);
            _repo.Verify(r => r.CreateMovie(It.Is<Movie>(m => m.Title == dto.Title && m.Director == dto.Director && m.ReleaseYear == dto.ReleaseYear && m.Genre == dto.Genre && m.Rating == dto.Rating)), Times.Once);
        }

        [Fact]
        public async Task CreateMovie_ShouldReturn400_WhenRepositoryReturnsNull()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var dto = new CreateMovieDTO
            {
                Title = "Nope"
            };

            _repo.Setup(r => r.CreateMovie(It.IsAny<Movie>())).ReturnsAsync((Movie)null).Verifiable();

            // Act 
            var result = await movieService.CreateMovie(dto);

            // Assert
            Assert.False(result.Status);
            Assert.Equal(400, result.StatusCode);
            Assert.Null(result.Data);
            _repo.Verify(r => r.CreateMovie(It.IsAny<Movie>()), Times.Once);
        }

        [Fact]
        public async Task CreateMovie_VerifyMapping_FromDtoToEntity()
        {
            // AAA- Arrange, Act, Assert
            // Arrange
            var dto = new CreateMovieDTO
            {
                Title = "M",
                Director = "D",
                ReleaseYear = 2001,
                Genre = "G",
                Rating = 5
            };
            Movie captured = null;

            _repo.Setup(r => r.CreateMovie(It.IsAny<Movie>())).Callback<Movie>(m => captured = m).ReturnsAsync(new Movie
            {
                Id = "1",
                Title = dto.Title,
            });

            // Act
            var result = await movieService.CreateMovie(dto);

            // Assert
            Assert.NotNull(captured);
            Assert.Equal(dto.Title, captured.Title);
            Assert.Equal(dto.Director, captured.Director);
            Assert.Equal(dto.ReleaseYear, captured.ReleaseYear);
            Assert.Equal(dto.Genre, captured.Genre);
            Assert.Equal(dto.Rating, captured.Rating);
        }

        //
        // READ SINGLE MOVIE: GetMovie(string id)
        //

        [Fact]
        public async Task GetMovie_ShouldReturn200WhenFound()
        {
            var movie = new Movie
            {
                Id = "1",
                Title = "X"
            };
            _repo.Setup(r => r.GetMovie("1")).ReturnsAsync(movie);

            // Act
            var result = await movieService.GetMovie("1");

            // Assert
            Assert.True(result.Status);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(movie, result.Data);
        }

        [Fact]
        public async Task GetMovie_ShouldReturn404WhenNotFound()
        {
            _repo.Setup(r => r.GetMovie("434")).ReturnsAsync((Movie)null);

            // Act
            var result = await movieService.GetMovie("434");

            // Assert
            Assert.False(result.Status);
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetMovie_PropagatesException_WhenRepositoryThrows()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            _repo.Setup(r => r.GetMovie(It.IsAny<string>())).ThrowsAsync(new InvalidOperationException("DB Error"));

            // Act & Assert
            await Assert.ThrowsAnyAsync<InvalidOperationException>(() => movieService.GetMovie("Xyz")); // random string as Id
        }

        //
        // READ ALL: GetMovies(int pageNumber, int pageSize)
        //

        [Fact]
        public async Task GetMovies_ShouldReturn200WithList_WhenRepositoryHasItems()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var movieList = new List<Movie> 
            {
                new Movie
                {
                    Id = "1",
                    Title = "A"
                },
                new Movie
                {
                    Id = "2",
                    Title = "B"
                }
            };
            _repo.Setup(r => r.GetMovies(1,10)).ReturnsAsync(movieList);

            // Act
            var result = await movieService.GetMovies(1, 10);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Status);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
        }

        [Fact]
        public async Task GetMovies_ShouldReturn404_WhenRepositoryReturnsEmptyList()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            _repo.Setup(r => r.GetMovies(1, 10)).ReturnsAsync(new List<Movie>());

            // Act
            var result = await movieService.GetMovies(1, 10);

            // Assert
            Assert.False(result.Status);
            Assert.Equal(404, result.StatusCode);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetMovies_ShouldDefaultPageNumberAndPageSize_WhenPassedZero()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var movieList = new List<Movie>
            {
                new Movie
                {
                    Id = "1",
                    Title = "A"
                },
                new Movie
                {
                    Id = "2",
                    Title = "B"
                }
            };
            _repo.Setup(r => r.GetMovies(1,10)).ReturnsAsync(movieList).Verifiable();

            // Act
            var result = await movieService.GetMovies(0,0);

            // Assert
            Assert.True(result.Status);
            _repo.Verify(r => r.GetMovies(1, 10), Times.Once);
        }

        //
        // UPDATE: UpdateMovie(string id, UpdateMovieDTO request)
        //

        [Fact]
        public async Task UpdateMovie_ShouldReturn400_WhenIdMismatch()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var dto = new UpdateMovieDTO
            {
                Id = "1",
                Title = "X"
            };

            // Act
            var result = await movieService.UpdateMovie("2", dto);

            // Assert
            Assert.False(result.Status);
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task UpdateMovie_ShouldReturn404_WhenMovieNotFound()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var dto = new UpdateMovieDTO { Id = "1", Title = "T" };
            _repo.Setup(r => r.GetMovie("1")).ReturnsAsync((Movie)null);

            // Act
            var result = await movieService.UpdateMovie("1", dto);

            // Assert
            Assert.False(result.Status);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task UpdateMovie_ShouldReturn200AndUpdatedMovie_WhenSuccessful()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            var existing = new Movie { Id = "1", Title = "Old", Director = "D", ReleaseYear = 2000, Genre = "G", Rating = 5 };
            var dto = new UpdateMovieDTO { Id = "1", Title = "New", Director = "D2", ReleaseYear = 2010, Genre = "G2", Rating = 8 };

            _repo.Setup(r => r.GetMovie("1")).ReturnsAsync(existing);
            _repo.Setup(r => r.UpdateMovie("1", It.IsAny<Movie>())).ReturnsAsync(new Movie
            {
                Id = "1",
                Title = "New",
                Director = "D2",
                ReleaseYear = 2010,
                Genre = "G2",
                Rating = 8
            });

            // Act
            var result = await movieService
                .UpdateMovie("1", dto);

            // Assert
            Assert.True(result.Status);
            Assert.Equal(200, result.StatusCode);
            Assert.NotNull(result.Data);
            Assert.Equal("New", result.Data.Title);
            _repo.Verify(r => r.UpdateMovie("1", It.Is<Movie>(m => m.Title == "New" && m.Director == "D2")), Times.Once);
        }

        //
        // DELETE: DeleteMovie(string id)
        //

        [Fact]
        public async Task DeleteMovie_ShouldReturn404_WhenMovieNotFound()
        {
            // AAA - Arrange, Act, Assert
            // Arrange
            _repo.Setup(r => r.GetMovie("xyz")).ReturnsAsync((Movie)null);

            // Act
            var result = await movieService.DeleteMovie("x");

            // Assert
            Assert.False(result.Status);
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task DeleteMovie_ShouldReturn200AndTrue_WhenDeleteSucceeds()
        {
            // Arrange
            var movie = new Movie { Id = "1", Title = "A" };
            _repo.Setup(r => r.GetMovie("1")).ReturnsAsync(movie);
            _repo.Setup(r => r.DeleteMovie("1")).ReturnsAsync(true);

            // Act
            var result = await movieService.DeleteMovie("1");

            // Assert
            Assert.True(result.Status);
            Assert.Equal(200, result.StatusCode);
            Assert.True(result.Data);
            _repo.Verify(r => r.DeleteMovie("1"), Times.Once);
        }
    }
}
