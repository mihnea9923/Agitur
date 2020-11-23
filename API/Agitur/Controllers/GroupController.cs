using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Agitur.ApplicationLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Agitur.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly GroupServices groupServices;

        public GroupController(GroupServices groupServices)
        {
            this.groupServices = groupServices;
        }

    }
}
