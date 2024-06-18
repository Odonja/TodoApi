using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

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
        private readonly IConfiguration Configuration;
        public ConfigController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // GET: api/Config
        [HttpGet]
        public async Task<ContentResult> GetConfig()
        {
            var myKeyValue = Configuration[MY_KEY];
            var title = Configuration[POSITION_TITLE];
            var name = Configuration[POSITION_NAME];
            var defaultLogLevel = Configuration[DEFAULT_LOGLEVEL];
            var moestuinGroente = Configuration[MOESTUIN_GROENTE_AARDAPPEL];
            var moestuinFruit = Configuration[MOESTUIN_FRUIT_FRAMBOOS];
            var horse = Configuration[HORSE];

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
