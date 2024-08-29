using Microsoft.AspNetCore.Mvc;
using WebApi.Abstractions;
using WebApi.Dto;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;

        public StoreController(IStoreRepository repository)
        {
            _storeRepository = repository;
        }

        [HttpPost("add_product_on_store")]
        public ActionResult<int> AddProductToStore(StoreDto storeItem)
        {
            var storeItemId = _storeRepository.AddStore(storeItem);
            return Ok(storeItemId);
        }

        [HttpGet("get_products_on_store")]
        public ActionResult<StoreDto> GetProductOnStore()
        {
            var storeItems = _storeRepository.GetStores();
            return Ok(storeItems);
        }
    }
}
