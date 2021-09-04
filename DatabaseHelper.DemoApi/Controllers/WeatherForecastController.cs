using DatabaseHelper.DemoApi.Models;
using DatabaseHelper.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DatabaseHelper.DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStoreProcedureHelper _spHelper;
        private readonly ICommandHelper _commandHelper;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, 
            IStoreProcedureHelper spHelper, ICommandHelper commandHelper)
        {
            _logger = logger;
            _spHelper = spHelper;
            _commandHelper = commandHelper;
        }

        [HttpGet]
        public IEnumerable<AnualIndicatorModel> Get()
        {
            _spHelper.ConnectionString = "Data Source=DESKTOP-5AQDVGF;Initial Catalog=FinancialCntrlDB;User ID=sa;Password=Mssql2020";
            _commandHelper.ConnectionString = "Data Source=DESKTOP-5AQDVGF;Initial Catalog=FinancialCntrlDB;User ID=sa;Password=Mssql2020";
            _commandHelper.ExecuteCommand("update FinancialEntities set Name = 'Santander Modified' where EntityId = @entityId", new { entityId = "524FA296-15EA-4DD5-AED0-00ED13632175" });
            return _spHelper.ExecuteRead<AnualIndicatorModel>("GetChartAnualIndicatorData", new { year = 2021 });
        }

        [HttpGet]
        [Route("ExecuteCommand")]
        public IActionResult ExecuteCommand(Guid? entityId)
        {
            _spHelper.ConnectionString = "Data Source=DESKTOP-5AQDVGF;Initial Catalog=FinancialCntrlDB;User ID=sa;Password=Mssql2020";
            _commandHelper.ExecuteCommand("update FinancialEntities set Name = 'Santander Modified' where EntityId = @entityId", new { entityId = entityId });

            return StatusCode(200);
        }
    }
}
