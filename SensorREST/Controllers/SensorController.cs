using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ModelLib;
using SensorREST.DBUTil;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private ManagerSensor mgr = new ManagerSensor();


        // GET: api/<SensorController>
        [HttpGet]
        public IEnumerable<SensorData> Get()
        {
            return mgr.HentAlle();
        }

        // GET api/<SensorController>/5
        [HttpGet("{id}")]
        public SensorData Get(int id)
        {
            return mgr.HentEn(id);
        }

        // POST api/<SensorController>
        [HttpPost]
        public void Post([FromBody] SensorData sensor)
        {
            mgr.OpretSensor(sensor);
        }

    }
}
