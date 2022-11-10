
using CarAuction.Entites;
using CarAuction.Entities.Action;
using CarAuction.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoAuction.Controller
{
    [Route("api/bid")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IBidService _service;

        public BidController(IBidService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> GetAll()
        {
            var vehicles = _service.GetAll();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public ActionResult<BidStatus> GetOne([FromRoute] int id)
        {
            var vehicles = _service.GetById(id);
            return Ok(vehicles);
        }

        [HttpDelete("{id}")]

        public ActionResult Delete([FromRoute] int id)
        {
            _service.Delete(id);

            return NotFound();
        }

        [HttpPost]
        public ActionResult CreateStudent([FromBody] CreateBidDto model)
        {

            var id = _service.Create(model);

            return Created($"/api/bid/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateBidDto model, [FromRoute] int id)
        {

            _service.Update(id, model);

            return Ok();
        }
    }
}
