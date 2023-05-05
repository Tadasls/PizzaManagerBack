using CompetitionEventsManager.Models;
using CompetitionEventsManager.Repository;
using CompetitionEventsManager.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Mime;
using CompetitionEventsManager.Models.Dto.OrderDTO;
using Microsoft.AspNetCore.JsonPatch;
using System.Security.Claims;
using CompetitionEventsManager.Services.Adapters.IAdapters;
using Microsoft.AspNetCore.Authorization;
using CompetitionEventsManager.Models.Dto.HorseDTO;
using CompetitionEventsManager.Services.Adapters;

namespace CompetitionEventsManager.Controllers
{
    /// <summary>
    /// This is Entries controls
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly ILogger<PizzaController> _logger;
        private readonly IOrderRepository _OrderRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IOrderAdapter _OrderAdapter;
        


        /// <summary>
        /// OrderController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="OrderAdapter"></param>
        public OrderController(ILogger<PizzaController> logger, IOrderRepository repository, IHttpContextAccessor httpContextAccessor, IOrderAdapter OrderAdapter)
        {
            _logger = logger;
            _OrderRepo = repository;
            _httpContextAccessor = new HttpContextAccessor();
            _OrderAdapter = OrderAdapter;
        }


        /// <summary>
        /// To get All Entries by User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("/GetAllEntriesForUser/{id:int}", Name = "GetAllEntries")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetOrderDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetOrderDTO>> GetUsersEntriesByUserId(int id)
        {
            //var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            //if (currentUserId != id)
            //{
            //    _logger.LogWarning("User {currentUserId} tried to access users {id} Entries", currentUserId, id);
            //    return Forbid();
            //}

            var userEnties = await _OrderRepo.GetAllFewDBAsync(x => x.UserId == id, new List<string>() { "LocalUser" });
            if (userEnties == null) return NotFound("User does not have Entries");


            return Ok(userEnties
             .Select(userEnties => new GetOrderDTO(userEnties))
             .ToList());
        }

        /// <summary>
        /// Fetches all registered Entries in the DB
        /// </summary>
        /// <param name="req"></param>
        /// <returns>All Entities</returns>
        [HttpGet("GetAllEntries")]
        //[Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetOrderDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllEntriesWithFilter([FromQuery] FilterOrderRequest req)
        {
            _logger.LogInformation("Getting Pizza list with parameters {req}", JsonConvert.SerializeObject(req));
            IEnumerable<Order> entities = await _OrderRepo.GetAllAsync();
            if (req.OrderID != null)
                entities = entities.Where(x => x.OrderID == req.OrderID);

            return Ok(entities
                .Select(d => new GetOrderDTO(d))
                .ToList());
        }



        /// <summary>
        /// Adding new Order into db
        /// </summary>
        /// <param name="orderDTO"></param>
        /// <returns>CreatedAtRoute with DTO</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("CreateOrder")]
        //[Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreateOrderDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CreateOrderDTO>> CreateOrder(CreateOrderDTO orderDTO)
        {
            if (orderDTO == null)
            {
                _logger.LogInformation("Method without data started at: ", DateTime.Now);
                return BadRequest("No data provided");
            }

            var order = new Order
            {
                OrderDate = DateTime.Now,
                Orders = orderDTO.Orders?.ToList(),
                TotalAmount = orderDTO.TotalAmount,

            };

            await _OrderRepo.CreateAsync(order);
            return CreatedAtRoute("GetOrder", new { Id = order.OrderID }, order);

        }





        /// <summary>
        /// UpdatePartialOrder with Patch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>No content</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("Patch/{id:int}", Name = "UpdatePartialOrder")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartialOrder(int id, [FromBody] JsonPatchDocument<Order> request)
        {
            if (id == 0 || request == null)
            {
                _logger.LogInformation("Method without data started at: ", DateTime.Now);
                return BadRequest("No data provided for update");
            }
            var OrderExists = await _OrderRepo.ExistAsync(d => d.OrderID == id);
            if (!OrderExists)
            {
                _logger.LogInformation("Order with id {id} not found", id);
                return NotFound("No such Order with ID was found");
            }
            var foundOrder = await _OrderRepo.GetAsync(d => d.OrderID == id);

            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != foundOrder.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Orders", currentUserId, id);
                return Forbid();
            }

            request.ApplyTo(foundOrder, ModelState);
            await _OrderRepo.UpdateAsync(foundOrder);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }


        /// <summary>
        ///  Update with Patch with DTO
        /// </summary>
        /// <param name="id">Order Id</param>
        /// <param name="request"> dto data for update</param>
        /// <returns>No Content</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("Patch/{id:int}/dto", Name = "UpdatePartialOrderDto")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartialOrderByDto(int id, [FromBody] JsonPatchDocument<UpdateOrderDTO> request)
        {
            if (id == 0 || request == null)
            {
                _logger.LogInformation("Method without data started at: ", DateTime.Now);
                return BadRequest("No data provided for update");
            }
            var OrderExists = await _OrderRepo.ExistAsync(d => d.OrderID == id);
            if (!OrderExists)
            {
                _logger.LogInformation("Order with id {id} not found", id);
                return NotFound("No such Order with ID was found");
            }

            var foundOrder = await _OrderRepo.GetAsync(d => d.OrderID == id, tracked: false);

            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != foundOrder.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Orders", currentUserId, id);
                return Forbid();
            }

            var updateOrderDto = _OrderAdapter.Bind(foundOrder);
            request.ApplyTo(updateOrderDto, ModelState);
            var Order = _OrderAdapter.Bind(updateOrderDto, foundOrder.OrderID);
            await _OrderRepo.UpdateAsync(Order);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }




        /// <summary>
        /// To delete Order
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        [HttpDelete("Order/delete/{id:int}")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            if (!await _OrderRepo.ExistAsync(d => d.OrderID == id))
            {
                _logger.LogInformation("Order with id {id} not found", id);
                return NotFound("No such ID Entries was found");
            }
            var Order = await _OrderRepo.GetAsync(d => d.OrderID == id);

            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != Order.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Entries", currentUserId, id);
                return Forbid();
            }

            await _OrderRepo.RemoveAsync(Order);
            return NoContent();
        }



                //papildomi variantai



        /// <summary>
        /// Fetches all Entries in the DB
        /// </summary>
        /// <returns>All Pizzas in DB</returns>
        [HttpGet("EntriesWithSQL")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetPizzaDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pizza>> GetEntries(int Id)
        {
            var data = _OrderRepo.GetSomeWithSQL(Id);

            return Ok(data); // grazina  objekta pagal ID
        }


        /// <summary>
        /// Fetches all Entries in the DB
        /// </summary>
        /// <returns>All Pizzas in DB</returns>
        [HttpGet("EntriesEager")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetPizzaDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Pizza>> GetEntriesEager()
        {

            var data = _OrderRepo.Getdata_With_EagerLoading();

            return Ok(data); //grazino visus entries su visais competitions /id cia nieko nedaro
        }

   

        /// <summary>
        /// Fetches all Entries in the DB
        /// </summary>
        /// <returns>All Pizzas in DB</returns>
        [HttpGet("PizzasFromEntriesAsyncFEWDB")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Order>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Order>> GetUserPizzaByOrderId(int id)
        {
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != id)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Pizzas", currentUserId, id);
                return Forbid();
            }

            var OrderPizzas = await _OrderRepo.GetAllFewDBAsync(x => x.UserId == id, new List<string> { "Pizza", "Rider" ,"Competition" });

            return Ok(OrderPizzas);//grazino Order klasu su Pizza klase 

        }






        /// <summary>
        /// Fetch Order with a specified ID from DB
        /// </summary>
        /// <param name="id">Requested Order ID</param>
        /// <returns>Pizza by specified ID</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("Order/{id:int}", Name = "GetOrder")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetOrderDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<GetOrderDTO>> GetOrderById(int id)
        {

            if (id == 0)
            {
                _logger.LogInformation("no id input");
                return BadRequest("Not entered ID");
            }
            if (!await _OrderRepo.ExistAsync(d => d.OrderID == id))
            {
                _logger.LogInformation("Order with id {id} not found", id);
                return NotFound("No such entries with this ID");
            }
            var Order = await _OrderRepo.GetAsync(d => d.OrderID == id);
            return Ok(new GetOrderDTO(Order));
        }







    }
}
