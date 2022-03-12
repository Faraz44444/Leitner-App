using System.ComponentModel;

namespace TagPortal.Domain.Enum
{
    public enum EnumCategoryPriority
    {
        [Description("Critical")]
        Critical = 1,

        [Description("High Priority")]
        HighPriority = 2,

        [Description("Normal")]
        Nomral = 3,
        
        [Description("Not Important")]
        NotImportant = 4,

        [Description("Extra")]
        Extra = 5,

        [Description("Unkown")]
        Unknown = 6,


    }
}
