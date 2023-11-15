using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum.OperationType
{
    public enum EnumOperationType
    {
        Create = 1,
        Update = 2,
        Delete = 3,
    }
    public static class EnumOperationTypeText
    {
        public static string Text(this EnumOperationType val)
        {
            switch (val)
            {
                case EnumOperationType.Create: return "Created";
                case EnumOperationType.Update: return "Updated";
                case EnumOperationType.Delete: return "Deleted";
                default: throw new InvalidDataException();
            }
        }
    }
}
