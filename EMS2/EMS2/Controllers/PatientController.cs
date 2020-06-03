using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMS2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private PatientDAL DAL;
        public PatientController(IConfiguration configuration)
        {
            var conn = configuration["ConnectionStrings:DefaultConnection"];
            DAL = PatientDAL.GetInst(conn);
        }
        // GET: api/<PatientController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            
            return new string[] { "value1", "value2" };
        }

        // GET api/<PatientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PatientController>
        [HttpPost]
        public void Post([FromBody] Patient value)
        {

        }

        // PUT api/<PatientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PatientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
