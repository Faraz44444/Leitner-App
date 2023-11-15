using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Attributes
{
    public abstract  class BaseEnumAttribute: Attribute
    {
        /// <summary>
        /// The Enum Value
        /// </summary>
        public int Id { get; set; }
    }

    public static class BaseEnumAttributeExtensions
    {
        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        public static T? Get<T>(this System.Enum enumVal) where T : BaseEnumAttribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            if (memInfo.Length == 0) return null;
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            var value = (attributes.Length > 0) ? (T)attributes[0] : null;
            if (value != null) value.Id = (int)System.Enum.Parse(type, enumVal.ToString());
            return value;
        }
    }
}
