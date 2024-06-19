using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : Controller
    {
        private const string MY_KEY = "MyKey";
        private const string POSITION_TITLE = "Position:Title";
        private const string POSITION_NAME = "Position:NamE";
        private const string DEFAULT_LOGLEVEL = "Logging:LogLevel:Default";
        private const string HORSE = "Horse";
        private const string MOESTUIN_GROENTE_AARDAPPEL = "Moestuin:Groente:Aardappel";
        private const string MOESTUIN_FRUIT_FRAMBOOS = "Moestuin:Fruit:Framboos";

        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration configuration;
        public ConfigController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // GET: api/Config
        [HttpGet]
        public async Task<ContentResult> GetConfig()
        {
            var myKeyValue = configuration[MY_KEY];
            var title = configuration[POSITION_TITLE];
            var name = configuration[POSITION_NAME];
            var defaultLogLevel = configuration[DEFAULT_LOGLEVEL];
            var moestuinGroente = configuration[MOESTUIN_GROENTE_AARDAPPEL];
            var moestuinFruit = configuration[MOESTUIN_FRUIT_FRAMBOOS];
            var horse = configuration[HORSE];

            var content = $"{MY_KEY}: {myKeyValue} \n" +
                          $"{POSITION_TITLE}: {title} \n" +
                          $"{POSITION_NAME}: {name} \n" +
                          $"{MOESTUIN_GROENTE_AARDAPPEL}: {moestuinGroente} \n" +
                          $"{MOESTUIN_FRUIT_FRAMBOOS}: {moestuinFruit} \n" +
                          $"{HORSE}: {horse} \n" +
                          $"{DEFAULT_LOGLEVEL}: {defaultLogLevel}";

            return await Task.FromResult(Content(content));
        }

    }
}
