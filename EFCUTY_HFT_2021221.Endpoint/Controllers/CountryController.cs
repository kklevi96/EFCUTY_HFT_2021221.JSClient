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
    public class CountryController : ControllerBase
    {
        ICountryLogic logic;
        IHubContext<SignalRHub> hub;

        public CountryController(ICountryLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }


        // GET: /country
        [HttpGet]
        public IEnumerable<Country> Get()
        {
            return this.logic.ReadAll();
        }

        // GET /country/[id]
        [HttpGet("{id}")]
        public Country Get(int id)
        {
            return this.logic.Read(id);
        }

        // POST /country
        [HttpPost]
        public void Post([FromBody] Country value)
        {
            this.logic.Create(value);
            this.hub.Clients.All.SendAsync("CountryCreated", value);
        }

        // PUT /country
        [HttpPut]
        public void Put([FromBody] Country value)
        {
            this.logic.Update(value);
            this.hub.Clients.All.SendAsync("CountryUpdated", value);

        }

        // /country/[id]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var countryToDelete = this.logic.Read(id);
            this.logic.Delete(id);
            this.hub.Clients.All.SendAsync("CountryDeleted", countryToDelete);
        }
    }
}
