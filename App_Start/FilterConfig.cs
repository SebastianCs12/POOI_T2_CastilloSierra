using System.Web;
using System.Web.Mvc;

namespace POOI_T2_CASTILLO_SIERRA
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
