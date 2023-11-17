using System;

namespace Domain.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class PermissionAttribute : BaseEnumAttribute
    {
        public bool IsVisible { get; set; }

        public PermissionAttribute(bool isVisible = true)
        {
            IsVisible = isVisible;
        }
    }
}
