using System.Configuration;
using TagPortal.Core;

namespace TbxPortal.Web.App_Start
{
    public class AppContextConfig
    {

        public static void InitServiceContext()
        {
            TagAppContext.Current = new TagAppContext(TagAppContext.GetServiceContext(ConfigurationManager.ConnectionStrings["TagPortal"].ConnectionString));

        }
    }

}