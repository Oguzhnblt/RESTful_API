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
    public class WriterController : ControllerBase
    {

        private readonly IGenericService<Writer> _writerRepository;
        private readonly IMapper _mapper;

        public WriterController(IGenericService<Writer> writerRepository, IMapper mapper)
        {
            _writerRepository = writerRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetWriters() // Bütün Writerleri listeleme 
        {
            var writers = await _writerRepository.GetAllAsync();

            var writersDtos = _mapper.Map<IEnumerable<WriterDTO>>(writers);

            return Ok(writersDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWriterById(int id) // ID'ye Göre Writer Listeleme 
        {
            var writer = await _writerRepository.GetByIdAsync(id);
            var writerDtos = _mapper.Map<WriterDTO>(writer);

            if (writerDtos == null)
            {
                throw new NotFoundException($"{typeof(Writer).Name}({id}) not found");
            }
            return Ok(writerDtos);
        }



        //[Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddWriter([FromBody] WriterDTO dto) // Writer Ekleme 
        {
            try
            {
                var validator = new WriterDTOValidator();
                var result = await validator.ValidateAsync(dto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                var writer = _mapper.Map<Writer>(dto);
                var gernreResult = await _writerRepository.AddAsync(writer);

                return Ok(gernreResult);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWriter(int id, [FromBody] WriterDTO dto) // ID'ye Göre Writer Güncelleme 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }

                var writer = await _writerRepository.GetByIdAsync(id);

                if (writer == null)
                {
                    return NotFound();
                }

                var validator = new WriterDTOValidator();
                var result = await validator.ValidateAsync(dto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _mapper.Map(dto, writer);
                await _writerRepository.UpdateAsync(writer);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWriter(int id) // ID'ye Göre Writer Silme 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }
                var writer = await _writerRepository.GetByIdAsync(id);

                var deletedWriter = await _writerRepository.DeleteAsync(writer);

                return Ok(deletedWriter);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // LİSTELEME VE SIRALAMA İŞLEVLERİ  -- İsim, Fiyat olarak sıralama ve listeleme yapma 

        [HttpGet("list")]
        public async Task<IActionResult> GetWriters([FromQuery] string name, [FromQuery] string sortOrder)
        {
            var writers = await _writerRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(name))
            {
                writers = writers.Where(p => p.WrieterFirstName.ToUpper().Contains(name.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Writer_Name":
                    writers = writers.OrderByDescending(p => p.WrieterFirstName);
                    break;
                case "Writer_asc":
                    writers = writers.OrderBy(p => p.WriterLastName);
                    break;
                case "Writer_desc":
                    writers = writers.OrderByDescending(p => p.WriterLastName);
                    break;
                default:
                    writers = writers.OrderBy(p => p.WriterLastName);
                    break;
            }

            return Ok(writers);
        }


    }
}

