using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StudyControlSoftware_API.Database.Models;
using StudyControlSoftware_API.Dto.Users;

namespace StudyControlSoftware_API.Filters
{
    public class EnsureUserNoExistsFilter : IAsyncActionFilter
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EnsureUserNoExistsFilter(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            UserRegisterDto? userRegisterDto = (UserRegisterDto?)context.ActionArguments["user"];

            string? username = userRegisterDto?.UserName;
            string? email = userRegisterDto?.Email;

            ApplicationUser? userByUsername = await _userManager.FindByNameAsync(username);
            ApplicationUser? userByEmail = await _userManager.FindByEmailAsync(email);

            if (username == null
                || userByUsername != null
                || userByEmail != null) context.Result = new BadRequestResult();
            else await next();
        }
    }
}
