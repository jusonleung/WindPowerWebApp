using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Description;
using WindPowerWebApp.Data;
using WindPowerWebApp.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WindPowerWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemDataController : ControllerBase
    {
        private readonly SqlDbService _sqlDbService;

        public SystemDataController(SqlDbService sqlDbService)
        {
            _sqlDbService = sqlDbService;
        }

        [HttpGet]
        public IEnumerable<DataModel> Get()
        {

            return _sqlDbService.GetAllSystemData();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Post(DataModel dataModel)
        {
            try
            {
                _sqlDbService.AddSystemData(dataModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
