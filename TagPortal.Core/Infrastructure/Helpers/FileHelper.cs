using System.Linq;
using TagPortal.Domain.Enum;

namespace TagPortal.Core.Infrastructure.Helpers
{
    public static class FileHelper
    {
        public static readonly string[] ImageTypes = { "png", "tif", "tiff", "bmp", "jpg", "jpeg" };

        public static EnumFileType GetFileType(string fileExtension)
        {
            fileExtension = fileExtension.TrimStart('.').ToLower();

            var fileType = EnumFileType.Unknown;
            if (ImageTypes.Contains(fileExtension)) fileType = EnumFileType.Image;
            else if (fileExtension == "pdf") fileType = EnumFileType.PDF;

            return fileType;
        }
    }
}
