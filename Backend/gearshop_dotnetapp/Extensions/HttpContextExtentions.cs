using gearshop_dotnetapp.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace gearshop_dotnetapp.Extensions
{
    public static class HttpContextExtentions
    {
        public static async Task RefreshLoginAsync(this HttpContext context)
        {
            
            if (context.User == null)
                throw new UnauthorizedAccessException();

            // The example uses base class, IdentityUser, yours may be called 
            // ApplicationUser if you have added any extra fields to the model
            var userManager = context.RequestServices
                .GetRequiredService<UserManager<User>>();
            var signInManager = context.RequestServices
                .GetRequiredService<SignInManager<User>>();

            User? user = await userManager.GetUserAsync(context.User);
            if (user == null) throw new UnauthorizedAccessException();
            if (signInManager.IsSignedIn(context.User))
            {
                await signInManager.RefreshSignInAsync(user);
                return;
            }
        }

       
    }
}
