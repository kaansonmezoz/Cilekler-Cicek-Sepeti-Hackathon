using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DenemeController : ControllerBase
    {
        [HttpGet]
        public string IsAlive() {
            return "SDKJFASKLDJFLKASDJFKLASJDF";
        }


    }
}