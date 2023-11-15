using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Database
{
    public class JoinType
    {
        public EnumJoinType Type { get; set; }
        public string Name { get; set; }

        public JoinType(EnumJoinType type)
        {
            Type = type;
            SetJoinTypeName();
        }

        public void SetJoinTypeName()
        {
            if (Type == EnumJoinType.LeftJoin) this.Name = "Left Join";
            if (Type == EnumJoinType.RightJoin) this.Name = "Right Join";
            if (Type == EnumJoinType.InnerJoin) this.Name = "Inner Join";
            if (Type == EnumJoinType.LeftOuterJoin) this.Name = "Left Outer Join";
            if (Type == EnumJoinType.RightOuterJoin) this.Name = "Right Outer Join";
            if (Type == EnumJoinType.FullJoin) this.Name = "Full Join";

        }

    }
    public enum EnumJoinType
    {
        LeftJoin = 1,
        RightJoin = 2,
        InnerJoin = 3,
        LeftOuterJoin = 4,
        RightOuterJoin = 5,
        FullJoin = 6,
    }
}
