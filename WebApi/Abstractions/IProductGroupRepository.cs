using WebApi.Dto;

namespace WebApi.Abstractions
{
    public interface IProductGroupRepository
    {
        public int AddGroup(ProductGroupDto group);
        public IEnumerable<ProductGroupDto> GetGroups();
    }
}
