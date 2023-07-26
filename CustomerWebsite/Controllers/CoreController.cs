using Microsoft.AspNetCore.Mvc;

namespace CustomerWebsite.Controllers
{
    public class CoreController : Controller
    {
        private const string OBJECT_ID_CLAIM = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public string Auth0UserId
        {
            get
            {
                return User.Claims.First(c => c.Type.Equals(OBJECT_ID_CLAIM, StringComparison.InvariantCultureIgnoreCase)).Value;
            }
        }

    }
}

