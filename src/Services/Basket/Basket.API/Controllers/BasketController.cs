using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    /// <summary>
    /// Basket Controller
    /// </summary>
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        /// <summary>
        /// Injecting `IBasketRepository`
        /// </summary>
        private readonly IBasketRepository _repository;
        public BasketController(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Get list of basket
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Return list of baskets</returns>
        [HttpGet("{userName}", Name = "GetBasket")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetBasket(string userName)
        {
            var response = await _repository.GetBasket(userName);

            // if the first client tries to add item into basket, will create new empty basket with the given username
            return Ok(response ?? new ShoppingCart(userName));
        }

        /// <summary>
        /// Will update basket and expecting entire basket object as parameter
        /// </summary>
        /// <param name="basket">Basket object</param>
        /// <returns>will updated basket object in json format</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await _repository.UpdateBasket(basket));
        }

        [HttpDelete("{userName}", Name = "DeleteBasket")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteBasket(string userName)
        {
            await _repository.DeleteBasket(userName);
            return Ok();
        }


    }
}
