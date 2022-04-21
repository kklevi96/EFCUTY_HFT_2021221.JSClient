using EFCUTY_HFT_2021221.Logic;
using EFCUTY_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace EFCUTY_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SettlementStatController : ControllerBase
    {
        ISettlementLogic sl;
        private readonly IHubContext<SignalRHub> hub;


        public SettlementStatController(ISettlementLogic sl, IHubContext<SignalRHub> hub)
        {
            this.sl = sl;
            this.hub = hub;
        }

        [HttpGet]
        public IEnumerable<Settlement> GoodSettlements()
        {
            return sl.GoodSettlements();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, double>> AvgHDIByCountries()
        {
            return sl.AvgHDIByCountries();
        }
    }
}
