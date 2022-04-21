using EFCUTY_HFT_2021221.Logic;
using EFCUTY_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace EFCUTY_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class CitizenStatController : ControllerBase
    {
        ICitizenLogic ctl;
        private readonly IHubContext<SignalRHub> hub;

        public CitizenStatController(ICitizenLogic ctl, IHubContext<SignalRHub> hub)
        {
            this.ctl = ctl;
            this.hub = hub;
        }
        
        [HttpGet]
        public IEnumerable<Citizen> PoorOldPeople()
        {
            return ctl.PoorOldPeople();
        }
        
        [HttpGet]
        public IEnumerable<Citizen> DevelopedCriminals()
        {
            return ctl.DevelopedCriminals();
        }
    }
}
