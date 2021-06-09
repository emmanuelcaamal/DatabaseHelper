using DatabaseHelper.DemoApi.Models;
using DatabaseHelper.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DatabaseHelper.DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStoreProcedureHelper _spHelper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IStoreProcedureHelper spHelper)
        {
            _logger = logger;
            _spHelper = spHelper;
        }

        [HttpGet]
        public IEnumerable<AnualIndicatorModel> Get()
        {
            return _spHelper.ExecReadProc<AnualIndicatorModel>("GetChartAnualIndicatorData", new { year = 2021 });
        }
    }
}
