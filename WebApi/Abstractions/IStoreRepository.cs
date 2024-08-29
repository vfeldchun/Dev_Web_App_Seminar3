using WebApi.Dto;

namespace WebApi.Abstractions
{
    public interface IStoreRepository
    {
        public IEnumerable<StoreDto> GetStores();
        public int AddStore(StoreDto store);
    }
}
