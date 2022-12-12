using DatabaseHelper.DemoApi.Models;
using DatabaseHelper.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DatabaseHelper.DemoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IStoreProcedureHelper _spHelper;
        private readonly ICommandHelper _commandHelper;
        private readonly ICommandTransactionHelper _commandTransactionHelper;

		public WeatherForecastController(ILogger<WeatherForecastController> logger,
			IStoreProcedureHelper spHelper, ICommandHelper commandHelper, ICommandTransactionHelper commandTransactionHelper)
		{
			_logger = logger;
			_spHelper = spHelper;
			_commandHelper = commandHelper;
			_commandTransactionHelper = commandTransactionHelper;
		}

		[HttpGet]
        public object Get()
        {
            //_spHelper.ConnectionString = "Data Source=localhost, 1433;Initial Catalog=CBLRDDB01_PETO;User ID=sa;Password=Mssql2020!";
            //_commandHelper.ConnectionString = "Data Source=localhost, 1433;Initial Catalog=CBLRDDB01_PETO;User ID=sa;Password=Mssql2020!";

            _commandTransactionHelper.SetConnection(new SqlConnection("Data Source=localhost, 1433;Initial Catalog=CBLRDDB01_PETO;User ID=sa;Password=Mssql2020!"));
            //_commandTransactionHelper.OpenTransaction(System.Data.IsolationLevel.RepeatableRead);

            //_commandTransactionHelper.ExecuteCommand("update Payments set UserUpdate = @userName, UpdateDate = getdate() where PaymentID = @entityId", new { entityId = "AE1BEA19-FBEB-4FB5-A0B3-A910010620DB", userName = "User3" });
            //_commandTransactionHelper.ExecuteCommand("update Payments set UserUpdate = @userName, UpdateDate = getdate() where PaymentID = @entityId", new { entityId = "91909DCD-9979-4E1E-87B4-A91001064AD2", userName = "User4" });
            //var payments = _commandTransactionHelper.ExecuteRead<object>("select * from Payments where FKOpeningBox = @opening", new { opening = "091E5F55-2ADB-4BEB-BDEB-A9100105ECB9" });

            //_commandTransactionHelper.CommitTransaction();
            var id = _commandTransactionHelper.ExecuteScalar(@"insert into TmpTest values('Emmanuel Caamal'); select SCOPE_IDENTITY();");


            return id;
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
