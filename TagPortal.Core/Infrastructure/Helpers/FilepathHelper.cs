using System;

namespace TagPortal.Core.Infrastructure.Helpers
{
    public static class FilepathHelper
    {
#if DEBUG

        private  static string WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;


        public static string SupplierArticleImagePath= WorkingDirectory + "\\App_Data\\SupplierArticleImages\\";



#else
        public const string SupplierArticleImagePath = @"D:\BP\SupplierArticleImages\";
#endif


    }
}
