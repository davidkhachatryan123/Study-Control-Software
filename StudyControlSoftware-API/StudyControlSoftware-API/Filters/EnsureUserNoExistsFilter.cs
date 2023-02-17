using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Filters
{
    public class EnsureUserExistsFilter : Attribute, IAsyncActionFilter
    {
        public bool No { get; set; } = false;

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UserManager<ApplicationUser>? _userManager =
                context.HttpContext.RequestServices.GetService<UserManager<ApplicationUser>>();

            UserRegisterDto? userRegisterDto = (UserRegisterDto?)context.ActionArguments["user"];

            string? username = userRegisterDto?.UserName;
            string? email = userRegisterDto?.Email;

            ApplicationUser? userByUsername = await _userManager!.FindByNameAsync(username);
            ApplicationUser? userByEmail = await _userManager!.FindByEmailAsync(email);

            if (userByUsername != null
                && userByEmail != null
                && !No)
                await next();
            else if (userByUsername == null
                && userByEmail == null
                && No)
                await next();
            else
                context.Result = new BadRequestResult();
        }
    }
}
