using gearshop_dotnetapp.Models.Identity;
using gearshop_dotnetapp.Resources;
using gearshop_dotnetapp.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace gearshop_dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddredssService _adressService;
        private readonly UserManager<User> _userManager;

        public AddressController(IAddredssService adressService, UserManager<User> userManager)
        {
            _adressService = adressService;
            _userManager = userManager;
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = _adressService.GetAddressById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> CreateAsync(SaveAddressResource saveAddressResource)
        {
            try
            {
                var userContext = HttpContext.User;
                var user = await _userManager.GetUserAsync(userContext);
                if (user == null)
                {
                    return Unauthorized();
                }
                var result = await _adressService.CreateAddressAsync(saveAddressResource, user);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<ActionResult> GetByUserId()
        {
            try
            {
                var userContext = HttpContext.User;
                var user = await _userManager.GetUserAsync(userContext);

                if (user == null )
                {
                    return Unauthorized();
                }
                
                var list = _adressService.GetAddressByUserIdAsync(user.Id);
                if (list == null) return NotFound();
                return Ok(list);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
