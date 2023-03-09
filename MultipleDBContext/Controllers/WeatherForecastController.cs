using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultipleDBContext.Model;
using MultipleDBContext.ViewModel;
using Microsoft.EntityFrameworkCore;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MultipleDBContext.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private Shefaa_Context _shefaaContext;
        private EgyptianCureBank_Context _EgyptianCureBankContext;
        private readonly IConfiguration _configuration;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(Shefaa_Context Shefaa_Context, EgyptianCureBank_Context EgyptianCureBank_Context,IConfiguration configuration)
        {
            _shefaaContext = Shefaa_Context;
            _EgyptianCureBankContext = EgyptianCureBank_Context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Govern_City>>> Get()
        {
            var Data = new List<Govern_City>();
            List<Governments> Government = await (from govern in _shefaaContext.Governments
                                              select new Governments
                                              {
                                                  Government_Id = govern.Government_Id,
                                                  Government_Name = govern.Government_Name,
                                              }).ToListAsync<Governments>();
            List<City>City = await (from city in _EgyptianCureBankContext.Cities
                                      select new City
                                      {
                                          Id = city.Id,
                                          CityName = city.CityName,
                                          RegonId = city.RegonId
                                      }).ToListAsync<City>();
            Data = (from g in Government
                    join c in City
                    on g.Government_Id equals c.RegonId
                    select new Govern_City
                    {
                        Government_Id = g.Government_Id,
                        Government_Name = g.Government_Name,
                        City_Id = c.Id,
                        CityName = c.CityName

                    }).ToList<Govern_City>();
                return Ok(Data);
        }
        [HttpGet]
        [Route("GetDataFromSQL")]
        public ActionResult<DataTable> GetFromSql()
        {
            DataTable dt = new DataTable();
            string Cmd = @"Select *
                        From Cities c Inner Join Shefaa_Bank.dbo.Governments g
                        On c.RegonId  = g.government_id ";
            using (SqlConnection Conn = new SqlConnection(_configuration.GetConnectionString("EgyptianCureBankConnection")))
            {
                Conn.Open();
                using (SqlCommand Com = new SqlCommand(Cmd, Conn))
                {
                    SqlDataAdapter Adapt = new SqlDataAdapter(Com);
                    Adapt.Fill(dt);
                }
            }
            return Ok(dt);
        }
    }
}
