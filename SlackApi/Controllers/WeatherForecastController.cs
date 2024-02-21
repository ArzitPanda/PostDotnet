using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SlackApi.Data;
using SlackApi.Data.Model;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SlackApi.Controllers
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
        private readonly SlackDbContext _slackDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,SlackDbContext slackDbContext)
        {
            _logger = logger;
            _slackDbContext = slackDbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get(int id)
        {
            using (var db = _slackDbContext)
            {
                var query = from relation in db.Relations
                            join user in db.Users on relation.UserId2 equals user.UserId
                            where relation.UserId1 == id
                            select new
                            {
                                User = user,
                                Relation = relation
                            };


                var query2 = await _slackDbContext.Relations.Include(relation => relation.User2).Where(u=>u.UserId1 == id).ToListAsync();

                var result = await query.ToListAsync();

                return Ok(query2);
            }
        }

    }
}
