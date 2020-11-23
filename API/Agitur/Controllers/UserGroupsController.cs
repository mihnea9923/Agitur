using Agitur.ApplicationLogic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agitur.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserGroupsController : ControllerBase
    {
        private readonly UserGroupServices userGroupServices;

        public UserGroupsController(UserGroupServices userGroupServices)
        {
            this.userGroupServices = userGroupServices;
        }
    }
}
