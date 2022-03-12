using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;
using TagPortal.Core;
using TagPortal.Core.Service.Security;
using TagPortal.Core.Service.User;
using TbxPortal.Web.Dto;
using TbxPortal.Web.Dto.Account;
using TbxPortal.Web.Dto.User;

namespace TbxPortal.Web.Controllers.Api
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseController
    {
        private SecurityService SecurityService => Services.SecurityService;
        private UserService UserService => Services.UserService;
        private UserSiteAccessService UserSiteAccessService => Services.UserSiteAccessService;
       
        [Route("")]
        [HttpGet]
        public IHttpActionResult GetMyAccount()
        {
            var user = UserService.GetById(CurrentUser.UserId);
            var dto = DataManagementMapper.Map<AccountDto>(user);
            if (user.UserType == TagPortal.Domain.Enum.EnumUserType.ClientUser)
            {
                var userSiteAccessList = UserSiteAccessService.GetList(new TagPortal.Core.Request.User.UserSiteAccessRequest() { UserId = CurrentUser.UserId });

                dto.UserSiteAccessList = DataManagementMapper.MapList<UserSiteAccessDto>(userSiteAccessList);
            }


            return Ok(dto);
        }

        [Route("")]
        [HttpPost]
        public IHttpActionResult UpdateMyAccount([FromBody] AccountDto dto)
        {
            if (dto == null) return BadRequest();
            var user = UserService.GetById(CurrentUser.UserId);
            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Email = dto.Email;
            user.CssFontSize = dto.CssFontSize;
            user.ForceUserInformationUpdate = false;

            UserService.Update(user);
            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updatePassword")]
        [HttpPost]
        public IHttpActionResult UpdateMyPassword([FromBody] UpdatePasswordRequestDto dto)
        {
            if (dto == null) return BadRequest();
            if (string.IsNullOrWhiteSpace(dto.NewPassword) || string.IsNullOrWhiteSpace(dto.ConfirmedNewPassword)) return BadRequest("Please fill in both new password and confirmed password.");
            if (dto.NewPassword.Trim().Length != dto.NewPassword.Length) return BadRequest("White space character is not allowed.");
            if (string.Compare(dto.NewPassword, dto.ConfirmedNewPassword, false) != 0) return BadRequest("New password and confirmed password are not the same.");


            SecurityService.UpdatePassword(CurrentUser.UserId, dto.OldPassword, dto.NewPassword);
            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updateCurrentSite")]
        [HttpPost]
        public IHttpActionResult UpdateMyCurrentSite([FromBody] UpdateCurrentSiteRequestDto dto)
        {
            if (dto == null || dto.NewCurrentSiteId < 1) return BadRequest();
            if (!CurrentUser.UserSiteAccessList.Any(x => x.SiteId == dto.NewCurrentSiteId)) return BadRequest();
            var user = UserService.GetById(CurrentUser.UserId);
            UserService.Update(user);

            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            var principal = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(CurrentUser.Username);
            var json = new JavaScriptSerializer().Serialize(principal.Identity.Name); //Could store more data to prevent fetching for every request.
            var ticket = new FormsAuthenticationTicket(1, principal.Identity.Name, DateTime.Now, DateTime.Now.AddHours(8), true, json);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.User = principal;
            HttpContext.Current.Response.Cookies.Add(cookie);

            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updateCurrentWarehouse")]
        [HttpPost]
        public IHttpActionResult UpdateMyCurrentWarehouse([FromBody] UpdateCurrentWarehouseRequestDto dto)
        {
            if (dto == null || dto.NewCurrentWarehouseId < 1) return BadRequest();

            var user = UserService.GetById(CurrentUser.UserId);

            UserService.Update(user);

            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            var principal = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(CurrentUser.Username);
            var json = new JavaScriptSerializer().Serialize(principal.Identity.Name); //Could store more data to prevent fetching for every request.
            var ticket = new FormsAuthenticationTicket(1, principal.Identity.Name, DateTime.Now, DateTime.Now.AddHours(8), true, json);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.User = principal;
            HttpContext.Current.Response.Cookies.Add(cookie);

            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updateCurrentSupplier")]
        [HttpPost]
        public IHttpActionResult UpdateMyCurrentSupplier([FromBody] UpdateCurrentSupplierRequestDto dto)
        {
            if (dto == null || dto.NewCurrentSupplierId < 1) return BadRequest();
            var user = UserService.GetById(CurrentUser.UserId);

            UserService.Update(user);

            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            var principal = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(CurrentUser.Username);
            var json = new JavaScriptSerializer().Serialize(principal.Identity.Name); //Could store more data to prevent fetching for every request.
            var ticket = new FormsAuthenticationTicket(1, principal.Identity.Name, DateTime.Now, DateTime.Now.AddHours(8), true, json);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.User = principal;
            HttpContext.Current.Response.Cookies.Add(cookie);

            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updateCurrentClient")]
        [HttpPost]
        public IHttpActionResult UpdateMyCurrentClient([FromBody] UpdateCurrentClientRequestDto dto)
        {
            if (dto == null || dto.NewCurrentClientId < 1) return BadRequest();
            var user = UserService.GetById(CurrentUser.UserId);
            UserService.Update(user);

            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            var principal = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(CurrentUser.Username);
            var json = new JavaScriptSerializer().Serialize(principal.Identity.Name); //Could store more data to prevent fetching for every request.
            var ticket = new FormsAuthenticationTicket(1, principal.Identity.Name, DateTime.Now, DateTime.Now.AddHours(8), true, json);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.User = principal;
            HttpContext.Current.Response.Cookies.Add(cookie);

            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updateUserType")]
        [HttpPost]
        public IHttpActionResult UpdateUserType()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            var principal = TagAppContext.Current.Services.SecurityService.GetTagPrincipal(CurrentUser.Username);
            var json = new JavaScriptSerializer().Serialize(principal.Identity.Name); //Could store more data to prevent fetching for every request.
            var ticket = new FormsAuthenticationTicket(1, principal.Identity.Name, DateTime.Now, DateTime.Now.AddHours(8), true, json);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            HttpContext.Current.User = principal;
            HttpContext.Current.Response.Cookies.Add(cookie);

            return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
        }

        [Route("updateLeftNavigationMinified")]
        [HttpPost]
        public IHttpActionResult UpdateLeftNavigationMinified([FromBody] UpdateLeftNavMinifiedRequestDto request)
        {
            try
            {
                UserService.UpdateLeftNavigationMinified(CurrentUser.UserId, request.LeftNavigationMinified);
                return Ok(new DefaultResponseDto(CurrentUser.UserId, true));
            }
            catch (FeedbackException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                LogException(ex);
                return InternalServerError();
            }
        }
    }
}