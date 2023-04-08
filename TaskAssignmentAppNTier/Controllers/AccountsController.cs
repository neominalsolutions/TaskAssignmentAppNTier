using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskAssignmentApp.Application.Dtos;
using TaskAssignmentApp.Persistance.ORM.EntityFramework.Identity;

namespace TaskAssignmentAppNTier.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountsController : ControllerBase
  {

    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<ApplicationRole> rolemanager;
    private readonly SignInManager<ApplicationUser> signInManager;


    public AccountsController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> rolemanager, SignInManager<ApplicationUser> signInManager)
    {
      this.userManager = userManager;
      this.rolemanager = rolemanager;
      this.signInManager = signInManager;
    }

    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestDto @request)
    {
      //this.userManager.
      //this.rolemanager.
      //this.signInManager.

      var user = new ApplicationUser();
      user.Email = request.Email;
      user.UserName = request.UserName;


      var result = await this.userManager.CreateAsync(user, request.Password);

      foreach (var role in request.Roles)
      {
        // user role assignment işlemi
         await this.userManager.AddToRoleAsync(user, role);
      }

      if (result.Succeeded)
      {
        return Created("", user.Id);
      }


      return BadRequest();
    }
  }
}
