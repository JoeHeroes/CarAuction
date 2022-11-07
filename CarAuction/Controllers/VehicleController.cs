using AutoAuction.Service;
using CarAuction.Entites;
using CarAuction.Entities.Action;
using Microsoft.AspNetCore.Mvc;

namespace AutoAuction.Controller
{
    [Route("api/vehicle")]
    [ApiController]
    public class VehicleController: ControllerBase
    {
        private readonly IVehicleService _service;

        public VehicleController(IVehicleService service)
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
        public ActionResult<Vehicle> GetOne([FromRoute] int id)
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
        public ActionResult CreateStudent([FromBody] CreateVehicleDto model)
        {

            var id = _service.Create(model);

            return Created($"/api/vehicle/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateVehicleDto model, [FromRoute] int id)
        {

            _service.Update(id, model);

            return Ok();
        }
    }
}
