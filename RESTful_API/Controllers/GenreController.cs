using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTful_API.BL.Abstract;
using RESTful_API.DTO.Entities;
using RESTful_API.DTO.Models;
using RESTful_API.Service.Validation;
using System.Data;

namespace RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {

        private readonly IGenericService<Genre> _genreRepository;
        private readonly IMapper _mapper;

        public GenreController(IGenericService<Genre> genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetGenres() // Bütün Genreleri listeleme 
        {
            var genres = await _genreRepository.GetAllAsync();

            var genresDtos = _mapper.Map<IEnumerable<GenreDTO>>(genres);

            return Ok(genresDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id) // ID'ye Göre Genre Listeleme 
        {
            var genre = await _genreRepository.GetByIdAsync(id);
            var genreDtos = _mapper.Map<GenreDTO>(genre);

            if (genreDtos == null)
            {
                throw new NotFoundException($"{typeof(Genre).Name}({id}) not found");
            }
            return Ok(genreDtos);
        }



        //[Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddGenre([FromBody] GenreDTO dto) // Genre Ekleme 
        {
            try
            {
                var validator = new GenreDTOValidator();
                var result = await validator.ValidateAsync(dto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                var genre = _mapper.Map<Genre>(dto);
                var gernreResult = await _genreRepository.AddAsync(genre);

                return Ok(gernreResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] GenreDTO dto) // ID'ye Göre Genre Güncelleme 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }

                var genre = await _genreRepository.GetByIdAsync(id);

                if (genre == null)
                {
                    return NotFound();
                }

                var validator = new GenreDTOValidator();
                var result = await validator.ValidateAsync(dto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _mapper.Map(dto, genre);
                await _genreRepository.UpdateAsync(genre);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id) // ID'ye Göre Genre Silme 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }
                var genre = await _genreRepository.GetByIdAsync(id);

                var deletedGenre = await _genreRepository.DeleteAsync(genre);

                return Ok(deletedGenre);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // LİSTELEME VE SIRALAMA İŞLEVLERİ  -- İsim, Fiyat olarak sıralama ve listeleme yapma 

        [HttpGet("list")]
        public async Task<IActionResult> GetGenres([FromQuery] string name, [FromQuery] string sortOrder)
        {
            var genres = await _genreRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(name))
            {
                genres = genres.Where(p => p.GenreName.ToUpper().Contains(name.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Genre_Name":
                    genres = genres.OrderByDescending(p => p.GenreName);
                    break;
                case "Title_asc":
                    genres = genres.OrderBy(p => p.IsActive);
                    break;
                case "Title_desc":
                    genres = genres.OrderByDescending(p => p.IsActive);
                    break;
                default:
                    genres = genres.OrderBy(p => p.GenreName);
                    break;
            }

            return Ok(genres);
        }


    }
}

