namespace Netrex.Frontend.Application.Commons.SharedClasses
{
    public static class PublicEndpoints
    {
        public static readonly HashSet<string> Routes = new(
            StringComparer.OrdinalIgnoreCase)
        {
            "Authentication/SignIn",
            "Authentication/SignUp",
            "Authentication/ForgotPassword",
            "Authentication/ResetPassword",
            "Authentication/VerifyEmail",
            "Products/GetHomepageProducts",
            "Products/GetCategories",
        };

        public static bool IsPublic(string endpoint)
            => Routes.Any(r =>
                endpoint.Contains(r,
                StringComparison.OrdinalIgnoreCase));
    }
}