using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RESTful_API.BL.Abstract;
using RESTful_API.DTO.Entities;
using RESTful_API.DTO.Models;
using RESTful_API.Service.Validation.BookValidator;

namespace RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IGenericService<Book> _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IGenericService<Book> bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetBooks() // Bütün ürünleri listeleme 
        {
            var books = await _bookRepository.GetAllAsync();

            var booksDtos = _mapper.Map<IEnumerable<BookDTO>>(books);

            return Ok(booksDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id) // ID'ye Göre Ürün Listeleme 
        {
            var book = await _bookRepository.GetByIdAsync(id);
            var bookDtos = _mapper.Map<BookDTO>(book);

            if (bookDtos == null)
            {
                throw new NotFoundException($"{typeof(Book).Name}({id}) not found");
            }
            return Ok(bookDtos);
        }



        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] BookDTO dto) // Ürün Ekleme 
        {
            try
            {
                var validator = new BookDTOValidator();
                var result = await validator.ValidateAsync(dto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                var book = _mapper.Map<Book>(dto);
                await _bookRepository.AddAsync(book);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO dto) // ID'ye Göre Ürün Güncelleme 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }

                var book = await _bookRepository.GetByIdAsync(id);

                if (book == null)
                {
                    return NotFound();
                }

                var validator = new BookDTOValidator();
                var result = await validator.ValidateAsync(dto);

                if (!result.IsValid)
                {
                    return BadRequest(result.Errors);
                }

                _mapper.Map(dto, book);
                await _bookRepository.UpdateAsync(book);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }



        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id) // ID'ye Göre Ürün Silme 
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id.");
                }
                var book = await _bookRepository.GetByIdAsync(id);

                var deletedBook = await _bookRepository.DeleteAsync(book);

                return Ok(deletedBook);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


        // LİSTELEME VE SIRALAMA İŞLEVLERİ  -- İsim, Fiyat olarak sıralama ve listeleme yapma 

        [HttpGet("list")]
        public async Task<IActionResult> GetBooks([FromQuery] string name, [FromQuery] string sortOrder)
        {
            var books = await _bookRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(name))
            {
                books = books.Where(p => p.BookName.ToUpper().Contains(name.ToUpper()));
            }

            switch (sortOrder)
            {
                case "Book_Name":
                    books = books.OrderByDescending(p => p.BookName);
                    break;
                case "Title_asc":
                    books = books.OrderBy(p => p.Title);
                    break;
                case "Title_desc":
                    books = books.OrderByDescending(p => p.Title);
                    break;
                default:
                    books = books.OrderBy(p => p.PublishDate);
                    break;
            }

            return Ok(books);
        }



    }
}