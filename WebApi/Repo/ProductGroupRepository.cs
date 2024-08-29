using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Abstractions;
using WebApi.Dto;
using WebApi.Models;

namespace WebApi.Repo
{
    public class ProductGroupRepository : IProductGroupRepository
    {
        private readonly ProductContext _context;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _cache;

        public ProductGroupRepository(ProductContext context, IMapper mapper, IMemoryCache cache)
        {
            _context = context;
            _mapper = mapper;
            _cache = cache;
        }

        public int AddGroup(ProductGroupDto group)
        {
            var newGroup = _mapper.Map<ProductGroup>(group);

            using (_context)
            {
                if (_context.ProductGroups.Any(x => x.Name.ToLower() == newGroup.Name.ToLower()))
                    throw new Exception("There is group with such name and price exists!");

                _context.ProductGroups.Add(newGroup);
                _context.SaveChanges();
                _cache.Remove("groups");
            }

            return newGroup.Id;
        }

        public IEnumerable<ProductGroupDto> GetGroups()
        {
            if (_cache.TryGetValue("groups", out List<ProductGroupDto> groupCacheList))
            {
                return groupCacheList;
            }

            using (_context)
            {
                var groupList = _context.ProductGroups.Select(_mapper.Map<ProductGroupDto>).ToList();
                _cache.Set("groups", groupList, TimeSpan.FromMinutes(30));
                return groupList;
            }

        }
    }
}
