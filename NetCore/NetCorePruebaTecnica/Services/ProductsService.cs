using AutoMapper;
using TechTest.DTO;
using TechTestData.Models;
using TechTestData.Repositories.Interface;
using TechTest.Services.Interface;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using TechTest.Configuration;

namespace TechTest.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IRepository<Products> _productsRepository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public ProductsService(IRepository<Products> productsRepository, IMapper mapper, IDistributedCache cache) 
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<ProductsDTO>> GetAllAsync()
        {
            var users = await _productsRepository.GetAllEntitiesAsync();
            return _mapper.Map<IEnumerable<ProductsDTO>>(users);
        }

        public async Task<ProductsDTO> GetByIdAsync(int id)
        {
            var cachedProduct = await _cache.GetAsync($"products-{id}");
            if (cachedProduct != null)
            {
                var serializedProductCache = Encoding.UTF8.GetString(cachedProduct);
                return JsonConvert.DeserializeObject<ProductsDTO>(serializedProductCache);
            }

            var product = await _productsRepository.GetByIdAsync(id);
            var productDTO = _mapper.Map<ProductsDTO>(product);
            var serializedProduct = JsonConvert.SerializeObject(productDTO);
            var encodedProduct = Encoding.UTF8.GetBytes(serializedProduct);
            await _cache.SetAsync($"products-{id}", encodedProduct, CacheOptions.DefaultExpiration);
            return productDTO;
        }

        public async Task<ProductsDTO> UpdateAsync(ProductsDTO productsDTO)
        {
            var product = _mapper.Map<Products>(productsDTO);
            var dbProduct = await _productsRepository.UpdateEntityAsync(product.Id, product);
            if (dbProduct != null)
                await _cache.RemoveAsync($"products-{dbProduct.Id}");
            return _mapper.Map<ProductsDTO>(dbProduct);
        }

        public async Task InsertAsync(ProductsDTO usersDTO)
        {
            var product = _mapper.Map<Products>(usersDTO);
            await _productsRepository.InsertEntityAsync(product);
        }

        public async Task<ProductsDTO> DeleteAsync(int id)
        {
            var dbProduct = await _productsRepository.DeleteEntityAsync(id);
            if (dbProduct != null)
                await _cache.RemoveAsync($"products-{dbProduct.Id}");
            return _mapper.Map<ProductsDTO>(dbProduct);
        }
    }
}