using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CNF.Admin.API.Controllers.Sys
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public abstract class ApiControllerBase : ControllerBase
    {

    }
}
