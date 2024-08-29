using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Abstractions;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Repo
{
    public class StoreRepository : IStoreRepository
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public StoreRepository(ProductContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddStore(StoreDto storeItem)
        {
            var newStoreItem = _mapper.Map<Store>(storeItem);

            using (_context)
            {
                _context.Stores.Add(newStoreItem);
                _context.SaveChanges();
                _cache.Remove("stores");
            }

            return newStoreItem.Id;
        }

        public IEnumerable<StoreDto> GetStores()
        {
            if (_cache.TryGetValue("stores", out List<StoreDto> stores))
            {
                return stores;
            }

            using (_context)
            {
                var storeList = _context.Stores.Select(_mapper.Map<StoreDto>).ToList();
                _cache.Set("stores", storeList, TimeSpan.FromMinutes(30));
                return storeList;
            }

        }
    }
}
