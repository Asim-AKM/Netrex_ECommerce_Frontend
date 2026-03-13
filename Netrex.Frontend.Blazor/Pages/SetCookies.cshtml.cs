using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Netrex.Frontend.Application.Commons.SharedClasses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Netrex.Frontend.Blazor.Pages
{
    [IgnoreAntiforgeryToken]
    public class SetCookiesModel : PageModel
    {
        public async Task<IActionResult> OnPostAsync([FromForm] string token, [FromForm] bool rememberMe)
        {
            if (string.IsNullOrEmpty(token))
                return Redirect("/login");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);
            var claims = jwt.Claims.ToList();
            claims.Add(new Claim(ClaimKey.Jwt, token));

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = rememberMe,
                    ExpiresUtc = DateTime.UtcNow.AddDays(7)
                }
            );

            var role = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


            return role switch
            {
                AppRoles.Customer => Redirect("/"),
                AppRoles.Admin => Redirect("/admindashboard"),
                AppRoles.Seller => Redirect("/SellerAndShopDetailsDashboard"),
                _ => Redirect("/401")
            };

        }
    }
}
