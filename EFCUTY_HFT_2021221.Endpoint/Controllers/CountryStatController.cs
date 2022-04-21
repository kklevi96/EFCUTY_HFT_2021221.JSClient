using EFCUTY_HFT_2021221.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace EFCUTY_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CountryStatController : ControllerBase
    {
        ICountryLogic cyl;
        private readonly IHubContext<SignalRHub> hub;

        public CountryStatController(ICountryLogic cyl, IHubContext<SignalRHub> hub)
        {
            this.cyl = cyl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> PoorCountries()
        {
            return cyl.PoorCountries();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> Population()
        {
            return cyl.Population();
        }
    }
}
