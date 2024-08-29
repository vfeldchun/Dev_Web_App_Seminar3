using WebApi.Abstractions;
using WebApi.Dto;

namespace WebApi.Mutations
{
    public class ProductMutation
    {
        public bool AddProduct([Service] IProductRepository repository, ProductDto product)
        {
            repository.AddProduct(product);
            return true;
        }

        public bool AddGroup([Service] IProductGroupRepository repository, ProductGroupDto group)
        {
            repository.AddGroup(group);
            return true;
        }

        // Дз Семинар 3
        public int AddProductToStore([Service] IStoreRepository repository, StoreDto storeItem)
        {
            var id = repository.AddStore(storeItem);
            return id;
        }
    }
}
