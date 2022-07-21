using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MonsterMexa.API.Contracts;
using MonsterMexa.BusinessLogic;
using MonsterMexa.Domain;
using MonsterMexa.API;

namespace MonsterMexa.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;
        private readonly ILogger<WarehouseController> _logger;

        public WarehouseController(IWarehouseService warehouseService, ILogger<WarehouseController> logger)
        {
            _warehouseService = warehouseService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(CreateProductRequest request, int quantity)
        {
            var product = Product.Create(request.Name, request.Size);

            if (product.IsFailure)
            {
                _logger.LogError(product.Error);
                return BadRequest(product.Error);
            }

            var id = await _warehouseService.AddProduct(product.Value, quantity);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _warehouseService.GetAll();

            return Ok(products);
        }

        [HttpGet("{productId:int}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var product = await _warehouseService.GetById(productId);

            if (product.IsFailure)
            {
                _logger.LogError(product.Error);
                return BadRequest(product.Error);
            }

            return Ok(product.Value);
        }

        [HttpPut("{productId:int},{quantity:int}")]
        public async Task<IActionResult> ChangeQuantity(int productId, int quantity)
        {
            var itemId = await _warehouseService.ChangeQuantity(productId, quantity);

            if (itemId.IsFailure)
            {
                _logger.LogError(itemId.Error);
                return BadRequest(itemId.Error);
            }

            return Ok(itemId.Value);
        }

        [HttpDelete("{productId:int}, {choice:bool}")]
        public async Task<IActionResult> HardDelete(int productId, bool choice)
        {
            if (!choice)
            {
                return BadRequest();
            }

            var result = await _warehouseService.HardDelete(productId);
            return Ok(result);
        }

        [HttpDelete("{productId:int}")]
        public async Task<IActionResult> SoftDelete(int productId)
        {
            var result = await _warehouseService.SoftDelete(productId);
            return Ok(result);
        }
    }
}
