using EFCUTY_HFT_2021221.Logic;
using EFCUTY_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EFCUTY_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitizenController : ControllerBase
    {
        ICitizenLogic logic;
        IHubContext<SignalRHub> hub;

        public CitizenController(ICitizenLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: /citizen
        [HttpGet]
        public IEnumerable<Citizen> Get()
        {
            return this.logic.ReadAll();
        }

        // GET /citizen/[id]
        [HttpGet("{id}")]
        public Citizen Get(int id)
        {
            return this.logic.Read(id);
        }

        // POST /citizen
        [HttpPost]
        public void Post([FromBody] Citizen value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CitizenCreated", value);
        }

        // PUT /citizen
        [HttpPut]
        public void Put([FromBody] Citizen value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CitizenUpdated", value);

        }

        // /citizen/[id]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var citizenToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CitizenDeleted", citizenToDelete);

        }
    }
}
