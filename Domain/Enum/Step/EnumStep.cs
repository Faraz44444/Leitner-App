using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum.OperationType
{
    public enum EnumStep
    {
        Box1_EveryDay = 1,
        Box2_EveryOtherDay = 2,
        Box3_Every4thDay= 3,
        Box4_Every7thDay= 4,
        Box5_Every14thDay= 5
    }
    public static class EnumStepText
    {
        public static string Text(this EnumStep val)
        {
            switch (val)
            {
                case EnumStep.Box1_EveryDay: return "Box 1 - Every Day";
                case EnumStep.Box2_EveryOtherDay: return "Box 2 - Every Other Day";
                case EnumStep.Box3_Every4thDay: return "Box 3 - Every 4th Day";
                case EnumStep.Box4_Every7thDay: return "Box 4 - Every 7th Day";
                case EnumStep.Box5_Every14thDay: return "Box 5 - Every 14th Day";
                default: throw new InvalidDataException();
            }
        }
    }
}
