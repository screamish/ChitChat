using System.Web;
using System.Web.Mvc;

namespace BCP.SimpleForum
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}