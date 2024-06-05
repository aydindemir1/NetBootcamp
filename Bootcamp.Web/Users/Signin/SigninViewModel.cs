using System.ComponentModel.DataAnnotations;

namespace Bootcamp.Web.Users.Signin
{
    public record SigninViewModel([Required] string Email, [Required] string Password, bool RememmberMe);
}
