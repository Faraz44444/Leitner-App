using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;

namespace Core.Infrastructure.Helpers
{
    public static class ImageHelper
    {
        public static string GetBase64String(string fileAbosultePath)
        {
            byte[] imageArray = File.ReadAllBytes(fileAbosultePath);
            string imageContent = Convert.ToBase64String(imageArray);

            var fileName = Path.GetFileName(fileAbosultePath);

            new FileExtensionContentTypeProvider().TryGetContentType(fileName, out string contentType);
            return $"data:{contentType};base64,{imageContent}";
        }
    }
}
