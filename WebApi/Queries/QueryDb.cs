using WebApi.Abstractions;
using WebApi.Dto;

namespace WebApi.Queries
{
    public class QueryDb
    {        
        public IEnumerable<ProductDto> GetProducts([Service] IProductRepository productRepository) => productRepository.GetProducts();
        public IEnumerable<StoreDto> GetStores([Service] IStoreRepository storeRepository) => storeRepository.GetStores();
        public IEnumerable<ProductGroupDto> GetGroups([Service] IProductGroupRepository groupRepository) => groupRepository.GetGroups();
    }
}
