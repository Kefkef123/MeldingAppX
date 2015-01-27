using System.Web.Http;
using MeldingAppX.Api;

namespace MeldingAppX
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
