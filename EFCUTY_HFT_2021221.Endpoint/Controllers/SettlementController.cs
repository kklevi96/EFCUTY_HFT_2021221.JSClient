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
    public class SettlementController : ControllerBase
    {
        ISettlementLogic logic;
        IHubContext<SignalRHub> hub;

        public SettlementController(ISettlementLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: /settlement
        [HttpGet]
        public IEnumerable<Settlement> Get()
        {
            return this.logic.ReadAll();
        }

        // GET /settlement/[id]
        [HttpGet("{id}")]
        public Settlement Get(int id)
        {
            return this.logic.Read(id);
        }

        // POST /settlement
        [HttpPost]
        public void Post([FromBody] Settlement value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("SettlementCreated", value);

        }

        // PUT /settlement
        [HttpPut]
        public void Put([FromBody] Settlement value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("SettlementUpdated", value);

        }

        // /settlement/[id]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var settlementToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("SettlementDeleted", settlementToDelete);
        }
    }
}
