using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WoodPelletsLib;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestWoodPellets.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WoodPelletsController : ControllerBase
    {
        WoodPelletRepository _woodPelletRepository = new WoodPelletRepository();

        public WoodPelletsController(WoodPelletRepository woodPelletRepository)
        {
            _woodPelletRepository = woodPelletRepository;
        }


        // GET: api/<WoodPelletsController>
        [HttpGet]
        public IEnumerable<WoodPellet> Get()
        {
            return _woodPelletRepository.Get();
        }

        // GET api/<WoodPelletsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<WoodPellet> Get(int id)
        {
            WoodPellet woodPellet = _woodPelletRepository.GetById(id);
            if (woodPellet == null) return NotFound("No Such Woodpellet, Id: " + id);
            return Ok(woodPellet);
        }

        // POST api/<WoodPelletsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        //[HttpPost]
        public ActionResult<WoodPellet> Post([FromBody] WoodPellet value)
        {
            if (value.Price >= 0)
            {
                WoodPellet woodPellet = _woodPelletRepository.Add(value);
                return CreatedAtAction("Get", new { id = woodPellet.Id }, woodPellet);
            }
            else
            {
                return BadRequest("Price must be a positive number");
            }
        }


        // PUT api/<WoodPelletsController>/5
        [HttpPut("{id}")]
        public WoodPellet Put(int id, [FromBody] WoodPellet value)
        {
            return _woodPelletRepository.Update(id, value);
        }

        // DELETE api/<WoodPelletsController>/5
        //[HttpDelete("{id}")]
        //public WoodPellet Delete(int id)
        //{
        //    return _woodPelletRepository
        //}
    }
}
