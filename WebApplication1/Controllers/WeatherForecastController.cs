using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Domain;
using WebApplication1.Filters;

namespace WebApplication1.Controllers
{
    //创建一个controller
    [ApiController]
    [Route("[controller]")]
    [Version1Discontinue] //resource filter:用于淘汰版本，可以检查uri中是否包含特定route来淘汰
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DemoContext _context;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, DemoContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        //[Route("/api/projects")] //可以用route来重覆盖默认route
        public IEnumerable<Club> Get()
        {
            var clubs = _context.Clubs.ToList();
            return clubs;
        }

        [HttpGet("{id}")]
        public IEnumerable<Club> GetById(int id)
        {
            var clubs = _context.Clubs
                .Where(x=>x.Id==id)
                .ToList();
            return clubs;
        }

        [HttpPost] //默认版本
        public IEnumerable<Player> Post([FromBody] Player player)
        //先会从body里绑定model，然后如果model类中有required一些要求，会进行验证（可以自定义验证），最后才执行函数体
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            var player1 = _context.Players.ToList();
            return player1;
        }

        //如果你现在有两个不同版本的验证，

        [HttpPost]
        [Route("/api/playerv1")] //V1会调player里面的验证--player_ensurebirthdate 
        public IEnumerable<Player> PostV1([FromBody] Player player)
        //先会从body里绑定model，然后如果model类中有required一些要求，会进行验证（可以自定义验证），最后才执行函数体
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            var player1 = _context.Players.ToList();
            return player1;
        }

        
        [HttpPost]
        [Route("/api/playerv2")]
        [Ticket_EnsureEnteredDate] //V2的验证是通过action filter -- Ticket_EnsureEnteredDate来执行的
        public IEnumerable<Player> PostV2([FromBody] Player player)
        //先会从body里绑定model，然后如果model类中有required一些要求，会进行验证（可以自定义验证），最后才执行函数体
        {
            _context.Players.Add(player);
            _context.SaveChanges();
            var player1 = _context.Players.ToList();
            return player1;
        }

    }
}
