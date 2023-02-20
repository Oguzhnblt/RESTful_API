using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using RESTful_API.BL.Abstract;
using RESTful_API.DTO.Entities;
using RESTful_API.DTO.Models;

namespace RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericService<Product> _productRepository;
        private readonly IMapper _mapper;

        public ProductController(IGenericService<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts() // Bütün ürünleri listeleme 
        {
            var products = await _productRepository.GetAllAsync();

            var productsDtos = _mapper.Map<IEnumerable<ProductDTO>>(products);

            return Ok(productsDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id) // ID'ye Göre Ürün Listeleme 
        {
            var product = await _productRepository.GetByIdAsync(id);
            var productDtos = _mapper.Map<ProductDTO>(product);

            if (productDtos == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) not found");
            }
            return Ok(productDtos);
        }



        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] ProductDTO dto) // Ürün Ekleme 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var addProduct = await _productRepository.AddAsync(_mapper.Map<Product>(dto));

            var productDto = _mapper.Map<ProductDTO>(addProduct);

            return Created("", new ProductDTO { Name = dto.Name, Price = dto.Price });
        }

        [Authorize(Roles = "Admin,User")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDTO dto) // ID'ye Göre Ürün Güncelleme 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productRepository.UpdateAsync(_mapper.Map<Product>(dto));
            return NoContent();
        }


        [Authorize(Roles = "Admin,User")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePartialProduct(int id, [FromBody] JsonPatchDocument<Product> product) // Ürünün belli bir özelliğini değiştirme
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingProduct = await _productRepository.GetByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            product.ApplyTo(existingProduct);

            await _productRepository.UpdateAsync(existingProduct);
            return NoContent();
        }

        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id) // ID'ye Göre Ürün Silme 
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            await _productRepository.DeleteAsync(product);
            return NoContent();
        }


        // LİSTELEME VE SIRALAMA İŞLEVLERİ  -- İsim, Fiyat olarak sıralama ve listeleme yapma 

        [HttpGet("list")]
        public async Task<IActionResult> GetProducts([FromQuery] string name, [FromQuery] string sortOrder)
        {
            var products = await _productRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.ToUpper().Contains(name.ToUpper()));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(p => p.Name);
                    break;
                case "price":
                    products = products.OrderBy(p => p.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(p => p.Price);
                    break;
                default:
                    products = products.OrderBy(p => p.Name);
                    break;
            }

            return Ok(products);
        }



    }
}