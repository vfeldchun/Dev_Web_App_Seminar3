using WebApi.Dto;

namespace WebApi.Abstractions
{
    public interface IProductRepository
    {        
        public void AddProduct(ProductDto product);
        public IEnumerable<ProductDto> GetProducts();
        public string GetProductsCsvString();
        public string GetCacheStatistics(string fileTitle);
    }
}
