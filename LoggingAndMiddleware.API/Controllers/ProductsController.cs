using LoggingAndMiddleware.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LoggingAndMiddleware.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(ILogger<ProductsController> logger, ILoggerFactory loggerFactory) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {

            var customILogger = loggerFactory.CreateLogger("CustomLogger");

            customILogger.LogInformation("This is a custom log message"); // custom log
            logger.LogInformation("This is a log message"); // default log
            // ILogger
            // Trace
            // Debug
            // Info
            // Warning
            // Error
            // Critical


            return Ok("Get all products");
        }

        [HttpPost]
        public IActionResult Post()
        {

            //var a = 10;
            //var b = 0;
            //var c = a / b;
            //throw new ExceptionSaveToDatabase("kritik hata");

            var response = new HttpClient().GetAsync("htts://www.google.com").Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new ExceptionSaveToDatabase("Error while creating product");
            }



            return Ok("product created");
        }
    }
}
