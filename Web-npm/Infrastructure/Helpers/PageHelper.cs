﻿using Core.Infrastructure.Security;
using Core.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Web.Infrastructure.Helpers
{
    public class CustomPageModel : PageModel
    {
        public string EntityName = "";
        public long EntityId = 0;

        protected ServiceContext Services => Core.AppContext.Current.Services;
        public UserIdentity CurrentUser => HttpContext.User.Identities.First().GetCurrentUser();
    }
}
