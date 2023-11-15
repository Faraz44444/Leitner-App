using System.IO;

namespace Domain.Enum.EntityType
{
    public enum EnumEntityType
    {
        User = 1,
        Role = 2,
        Client = 3
    }
    public static class EnumEntityTypeText
    {
        public static string Text(this EnumEntityType val)
        {
            switch(val)
            {
                case EnumEntityType.User: return "User";
                case EnumEntityType.Role: return "Role";
                case EnumEntityType.Client: return "Client";
                default: throw new InvalidDataException();
            }
        }
    }
}
