using TagPortal.Core;

namespace TagPortal.DataMigrator
{
    public class Configuration
    {
        public static void Configure(string tagPortalConnectionString)
        {
            if (TagAppContext.Current == null)
                TagAppContext.Current = new TagAppContext(TagAppContext.GetServiceContext(tagPortalConnectionString));
        }
    }
}
