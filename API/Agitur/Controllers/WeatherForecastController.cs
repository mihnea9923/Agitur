using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agitur.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "alive";
        }
        [HttpPost]
        public IActionResult Post([FromForm] IFormFile myFile)
        {

            var memory = new MemoryStream();
            myFile.CopyTo(memory);
            memory.Position = 0;
            return File(memory, "audio/wav", myFile.Name);
        }

        public class Blob
        {
            public byte[] blob { get; set; }
        }
    }
}
