using CompetitionEventsManager.Models;
using CompetitionEventsManager.Models.Dto.HorseDTO;
using CompetitionEventsManager.Repository.IRepository;
using CompetitionEventsManager.Services.Adapters.IAdapters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;


namespace CompetitionEventsManager.Controllers
{
    /// <summary>
    /// Pizza Controlers
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly ILogger<PizzaController> _logger;
        private readonly IPizzaRepository _PizzaRepo;
        private readonly IPizzaAdapter _PizzaAdapter;
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// this is Pizza Controlller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <param name="PizzaAdapter"></param>
        public PizzaController(ILogger<PizzaController> logger, IPizzaRepository repository, IPizzaAdapter PizzaAdapter, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _PizzaRepo = repository;
            _PizzaAdapter = PizzaAdapter;
            _httpContextAccessor = httpContextAccessor;
        }


        /// <summary>
        /// Fetches all Pizzas in the DB
        /// </summary>
        /// <returns>All Pizzas in DB</returns>
        [HttpGet("/GetAllPizzas/{id:int}", Name = "GetAllPizzas")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetPizzaDTO>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<GetPizzaDTO>> GetUserPizzasById(int id)
        {

           // var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
           //if (currentUserId != id)
           // {
           //     _logger.LogWarning("User {currentUserId} tried to access user {id} Pizzas", currentUserId, id);
           //     return Forbid();
           // }

            var OrderPizzas = await _PizzaRepo.GetAllFewDBAsync(x => x.UserId == id, new List<string>(){ "LocalUser" });
            if (OrderPizzas == null) return NotFound("User does not have an Pizzas");


            return Ok(OrderPizzas
             .Select(OrderPizzas => new GetPizzaDTO(OrderPizzas))
             .ToList());
        }



        /// <summary>
        /// Fetch registered Pizza with a specified ID from DB
        /// </summary>
        /// <param name="id">Requested Pizza ID</param>
        /// <returns>Pizza by specified ID</returns>
        /// <response code="200">OK</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Client could not authenticate a request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("Pizzas/{id:int}", Name = "GetPizza")]
        //[Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetPizzaDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<GetPizzaDTO>> GetPizzaById(int id)
        {     

            if (id == 0)
            {
                _logger.LogInformation("no id input");
                return BadRequest("Not entered ID");
            }         
            if (!await _PizzaRepo.ExistAsync(d => d.PizzaID == id))
            {
                _logger.LogInformation("Pizza with id {id} not found", id);
                return NotFound("No such entries with this ID");
            }
            var Pizza = await _PizzaRepo.GetAsync(d => d.PizzaID == id);
            return Ok(new GetPizzaDTO(Pizza));
        }

        /// <summary>
        /// Fetches all registered Pizzas in the DB
        /// </summary>
        /// <param name="req"></param>
        /// <returns>All Entities</returns>
        /// <response code="200">OK</response>
        /// <response code="500">Internal server error</response>
        [HttpGet("GetAllPizzas")]
        //[Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetPizzaDTO>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllPizzasWithFilter([FromQuery] FilterPizzaRequest req)
        {
            _logger.LogInformation("Getting Pizza list with parameters {req}", JsonConvert.SerializeObject(req));
            IEnumerable<Pizza> entities = await _PizzaRepo.GetAllAsync();
            if (req.PizzaName != null)
                entities = entities.Where(x => x.PizzaName == req.PizzaName);
            return Ok(entities
                .Select(d => new GetPizzaDTO(d))
                .ToList());
        }



        /// <summary>
        /// Adding new Pizza into db
        /// </summary>
        /// <param name="PizzaDTO">New Pizza data</param>
        /// <returns>CreatedAtRoute with DTO</returns>
        /// <response code="201">Created</response>
        /// <response code="400">Bad request</response>
        /// <response code="500">Internal server error</response>
        [HttpPost("CreatePizza")]
        //[Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CreatePizzaDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<ActionResult<CreatePizzaDTO>> CreatePizza([FromBody] CreatePizzaDTO PizzaDTO)
        {
            if (PizzaDTO == null)
            {
                _logger.LogInformation("Method without data started at: ",  DateTime.Now);
                return BadRequest("No data provided");
            }
            //var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            //if (currentUserId == null )
            //{
            //    return Forbid();
            //}


            Pizza model = new Pizza()
            {
            PizzaName = PizzaDTO.PizzaName,
            Size = PizzaDTO.Size,
            Toppings = PizzaDTO.Toppings,
            Price = PizzaDTO.Price,


                //UserId = currentUserId,
            };
            await _PizzaRepo.CreateAsync(model);
            return CreatedAtRoute("GetPizza", new { Id = model.PizzaID }, PizzaDTO);
        }


        /// <summary>
        /// Pizza update place 
        /// </summary>
        /// <param name="id">specify which Order to update</param>
        /// <param name="updatePizzaDTO"> DTo with specific properties</param>
        /// <returns>No content if update is Ok</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPut("Pizzas/update/{id:int}")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePizza(int id, [FromBody] UpdatePizzaDTO updatePizzaDTO)
        {
            if (id == 0 || updatePizzaDTO == null)
            {
                _logger.LogInformation("no data imputed");
                return BadRequest("No data was provided");
            }

            var foundPizza = await _PizzaRepo.GetAsync(d => d.PizzaID == id);
            if (foundPizza == null)
            {
                _logger.LogInformation("Pizza with id {id} not found", id);
                return NotFound("No such entries with this ID");
            }
            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != foundPizza.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Pizzas", currentUserId, id);
                return Forbid();
            }


            foundPizza.PizzaName = updatePizzaDTO.PizzaName;


            await _PizzaRepo.UpdateAsync(foundPizza);
            return NoContent();
        }


        /// <summary>
        /// UpdatePartialPizza with Patch
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns>No content</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("Patch/{id:int}", Name = "UpdatePartialPizza")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartialPizza( int id, [FromBody] JsonPatchDocument<Pizza> request)
        {
            if (id == 0 || request == null)
            {
                _logger.LogInformation("Method without data started at: ", DateTime.Now);
                return BadRequest("No data provided for update");
            }
            var PizzaExists = await _PizzaRepo.ExistAsync(d => d.PizzaID == id);
            if (!PizzaExists)
            {
                _logger.LogInformation("Pizza with id {id} not found", id);
                return NotFound("No such Pizza with ID was found");
            }
            var foundPizza = await _PizzaRepo.GetAsync(d => d.PizzaID == id);

            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != foundPizza.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Pizzas", currentUserId, id);
                return Forbid();
            }


            request.ApplyTo(foundPizza, ModelState);
            await _PizzaRepo.UpdateAsync(foundPizza);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        /// <summary>
        ///  Update with Patch with DTO
        /// </summary>
        /// <param name="id">Pizza Id</param>
        /// <param name="request"> dto data for update</param>
        /// <returns>No Content</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpPatch("Patch/{id:int}/dto", Name = "UpdatePartialPizzaDto")]
        [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdatePartialPizzaByDto( int id, [FromBody] JsonPatchDocument<UpdatePizzaDTO> request)
        {
            if (id == 0 || request == null)
            {
                _logger.LogInformation("Method without data started at: ", DateTime.Now);
                return BadRequest("No data provided for update");
            }
            var PizzaExists = await _PizzaRepo.ExistAsync(d => d.PizzaID == id);
            if (!PizzaExists)
            {
                _logger.LogInformation("Pizza with id {id} not found", id);
                return NotFound("No such Pizza with ID was found");
            }

            var foundPizza = await _PizzaRepo.GetAsync(d => d.PizzaID == id, tracked: false);

            var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            if (currentUserId != foundPizza.UserId)
            {
                _logger.LogWarning("User {currentUserId} tried to access user {id} Pizzas", currentUserId, id);
                return Forbid();
            }

            var updatePizzaDto = _PizzaAdapter.Bind(foundPizza);
            request.ApplyTo(updatePizzaDto, ModelState);
            var Pizza = _PizzaAdapter.Bind(updatePizzaDto, foundPizza.PizzaID);
            await _PizzaRepo.UpdateAsync(Pizza);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }

        /// <summary>
        /// To delete Pizza
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No Content</returns>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad request</response>
        /// <response code="404">Page Not Found</response>
        /// <response code="500">Internal server error</response>
        [HttpDelete("Pizzas/delete/{id:int}")]
       [Authorize(Roles = "admin,user")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeletePizza( int id)
        {
            if (!await _PizzaRepo.ExistAsync(d => d.PizzaID == id))
            {
                _logger.LogInformation("Pizza with id {id} not found", id);
                return NotFound("No such ID Entries was found");
            }
            var Pizza = await _PizzaRepo.GetAsync(d => d.PizzaID == id);

            //var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.Identity.Name);
            //if (currentUserId != Pizza.UserId)
            //{
            //    _logger.LogWarning("User {currentUserId} tried to access user {id} Pizzas", currentUserId, id);
            //    return Forbid();
            //}

            await _PizzaRepo.RemoveAsync(Pizza);
            return NoContent();
        }



     
    }











}

