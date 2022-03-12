using System.ComponentModel;

namespace TagPortal.Domain.Enum
{
    public enum EnumFileType
    {
        [Description("")]
        Unknown = 1,

        [Description("pdf")]
        PDF = 2,

        [Description("image")]
        Image = 3,
    }
}
