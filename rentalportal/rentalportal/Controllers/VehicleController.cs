using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using rentalportal.api.Filters;
using rentalportal.model.Core;
using rentalportal.model.Domain;

namespace rentalportal.api.Controllers
{
    //TODO create VMs and mappings
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private readonly IRepository<Vehicle> _vehicleRepository;

        public VehicleController(IRepository<Vehicle> vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            //var rentalPeriods = _rentalPeriodRepository.Items.ToList();
            //return Ok(rentalPeriods.Select(x => x.From.ToString()));
            return Ok(_vehicleRepository.Items);
        }

        [HttpGet("{id}")]
        public ActionResult<Vehicle> Get(Guid id)
        {
            var rentalPeriod = _vehicleRepository.ItemsNoTracking.FirstOrDefault(x => x.Id == id);
            return Ok(rentalPeriod);
        }

        [HttpPost]
        [TransactionFilter]
        public void Post([FromBody] Vehicle vehicle)
        {
            vehicle.Id = Guid.NewGuid();
            _vehicleRepository.Add(vehicle);
        }
    }
}
