using Core.Infrastructure.Security;
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
            Items.Add(new NavigationItem("Materials", "/material/materialList", "", "fa fa-clipboard"));
            Items.Add(new NavigationItem("Categories", "/category/categoryList", "", "fa-solid fa-tags"));
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

        public string[] RouteArray => RouteAlias != null ? RouteAlias.Replace("index", "").Split("/") : Array.Empty<string>();

        public NavigationItem(string name, string route, string routeAlias, string icon)
        {
            Name = name;
            Route = route;
            RouteAlias = routeAlias ?? route;
            Icon = icon;
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
            bool requireAllPermissions = true)
        {
            var child = new NavigationItem(name, route, aliasroute, icon);
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
                if (RouteArray[i] != currentRouteArray[i] || (currentRouteArray.Length != RouteArray.Length && !currentRouteArray.All(x => x == "")))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
