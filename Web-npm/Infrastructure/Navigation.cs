using Core.Infrastructure.Security;
using Domain.Enum.Permission;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Web.Infrastructure
{
    public static class Navigation
    {
        public static List<NavigationItem> Items = new();

        static Navigation()
        {
            Items.Add(new NavigationItem("Dashboard", "/index", "", "fa fa-tachometer-alt"));
            
            var payments = new NavigationItem("Data Management", "fa-solid fa-receipt");

            payments.AddChild("Payment", "fa-solid fa-money-bill-transfer",
                "/datamanagement/payment/paymentlist", "/datamanagement/payment/paymentlist");
            payments.AddChild("Payment Total", "fa-solid fa-money-bill-transfer",
                "/datamanagement/payment/paymenttotal/paymenttotallist", "/datamanagement/payment/paymenttotal/paymenttotallist");
            payments.AddChild("Business", "fa-solid fa-suitcase",
                "/datamanagement/business/businesslist", "/datamanagement/business/businesslist");
            payments.AddChild("Category", "fa-solid fa-tags",
                "/datamanagement/category/categorylist", "/datamanagement/category/categorylist");
            payments.AddChild("Payment Priority", "fa-solid fa-land-mine-on",
                "/datamanagement/payment/paymentpriority/paymentprioritylist", "/datamanagement/payment/paymentpriority/paymentprioritylist");
            Items.Add(payments);

            var reports = new NavigationItem("Reports", "fa-solid fa-bullhorn");

            reports.AddChild("Yearly Overview", "fa-solid fa-magnifying-glass",
                "/report/yearlyoverview", "/report/yearlyoverview");
            reports.AddChild("Monthly Overview", "fa-solid fa-magnifying-glass",
                "/report/monthlyoverview", "/report/monthlyoverview");
            Items.Add(reports);

            var admin = new NavigationItem("Admin", "fa-solid fa-gears");
            admin.AddChild("Users", "fa-solid fa-users", 
                "/admin/account/user/userlist", "/admin/account/user/userlist");
            admin.AddChild("Roles", "fa-solid fa-users-gear", 
                "/admin/account/role/rolelist", "/admin/account/role/rolelist");
            Items.Add(admin);
        }
    }

    public class NavigationItem
    {
        public string Name { get; set; }
        public string Route { get; set; }
        public string RouteAlias { get; set; }
        public string Icon { get; set; }
        public List<NavigationItem> Children { get; set; }
        private bool RequireAllPermissions { get; set; }
        private EnumPermission[] RequiredPermissions { get; set; }

        public string[] RouteArray => RouteAlias != null ? RouteAlias.Replace("index", "").Split("/") : Array.Empty<string>();

        public NavigationItem(string name, string route, string routeAlias, string icon,
            bool requireAllPermissions = true, EnumPermission[] requiredPermissions  = null)
        {
            Name = name;
            Route = route;
            RouteAlias = routeAlias ?? route;
            Icon = icon;
            RequireAllPermissions = requireAllPermissions;
            RequiredPermissions = requiredPermissions;
        }

        public NavigationItem(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }

        public void AddChild(
            string name,
            string icon,
            string route,
            string aliasroute,
            EnumPermission[] permissions = null,
            bool requireAllPermissions = true)
        {
            var child = new NavigationItem(name, route, aliasroute, icon, requireAllPermissions, permissions);
            if (Children == null)
            {
                Children = new List<NavigationItem>();
            }
            Children.Add(child);
        }

        public bool IsActiveRoute(string[] currentRouteArray)
        {
            if (RouteArray == null || RouteArray.Length == 0)
            {
                return false;
            }
            if (currentRouteArray.Length == 0)
            {
                return false;
            }
            for (var i = 0; i < RouteArray.Length; i++)
            {
                if (RouteArray[i] != currentRouteArray[i] || (currentRouteArray.Length != RouteArray.Length && !currentRouteArray.All(x=> x == "")))
                {
                    return false;
                }
            }
            return true;
        }
        public bool HasAccess(UserIdentity claimsIdentity)
        {

            if (RequiredPermissions != null && (Children?.Count() ?? 0) != 0)
                throw new ArgumentException("A Group may not contain permissions");

            if ((Children?.Count() ?? 0) > 0)
                return Children.Any(x => x.HasAccess(claimsIdentity));

            return RequireAllPermissions ?
                claimsIdentity.HasAllPermissions(RequiredPermissions) :
                claimsIdentity.HasAnyPermissions(RequiredPermissions);
        }
    }
}
