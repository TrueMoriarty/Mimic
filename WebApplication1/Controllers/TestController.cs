using DAL;
using DAL.EfClasses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MimicWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpPost]
        public IActionResult Create([FromBody] UserDTO user)
        {
            unitOfWork.UserRepository.Insert(user.ToUser());
            unitOfWork.Save();
            return Ok();
        }
    }
}
