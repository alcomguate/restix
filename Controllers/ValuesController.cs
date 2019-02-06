using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RESTXI1.Models;
using RESTXI1.Services;

namespace RESTXI1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> Get()
        {
            var clienteService = new ClientServices();
            {
                var cliente = clienteService.GetClients();
                if (cliente != null)
                {
                    return Ok(cliente);
                }

                return NotFound("Messaje: The is no clients available");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Cliente> Get(int id)
        {
            var clienteService = new ClientServices();
            {
                var cliente = clienteService.GetClientById(id);
                if (cliente != null)
                {
                    return Ok(cliente);
                }

                return NotFound("Messaje: The is no client, with id:" + id);
            }
        }

        [HttpGet()]
        [Route("ASYNC")]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetAsync()
        {
            var clienteService = new ClientServices();
            {
                var cliente = await clienteService.GetClientsAsync();
                if (cliente != null)
                {
                    return Ok(cliente);
                }

                return NotFound("Messaje: The is no clients available");
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] Cliente cliente)
        {
            try
            {
                var clienteService = new ClientServices();
                {
                    clienteService.AddClient(cliente);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Cliente cliente)
        {
            try
            {
                var clienteService = new ClientServices();
                {
                    clienteService.UpdateClient(cliente, id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            try
            {
                var clienteService = new ClientServices();
                {
                    clienteService.DeleteClient(id);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
