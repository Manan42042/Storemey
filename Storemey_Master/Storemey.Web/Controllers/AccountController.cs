using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.UI;
using Abp.Web.Models;
using Storemey.Authorization;
using Storemey.Authorization.Roles;
using Storemey.Authorization.Users;
using Storemey.MultiTenancy;
using Storemey.Sessions;
using Storemey.Web.Controllers.Results;
using Storemey.Web.Models;
using Storemey.Web.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Storemey.MultiTenancy.Dto;
using Hangfire;

namespace Storemey.Web.Controllers
{
    public class AccountController : StoremeyControllerBase
    {
        private readonly TenantManager _tenantManager;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly LogInManager _logInManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly ILanguageManager _languageManager;
        private readonly ITenantCache _tenantCache;

        private readonly ITenantAppService _tenantAppService;


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(
            TenantManager tenantManager,
            UserManager userManager,
            RoleManager roleManager,
            IUnitOfWorkManager unitOfWorkManager,
            IMultiTenancyConfig multiTenancyConfig,
            LogInManager logInManager,
            ISessionAppService sessionAppService,
            TenantAppService tenantAppService,
            ILanguageManager languageManager,
            ITenantCache tenantCache)
        {
            _tenantManager = tenantManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWorkManager = unitOfWorkManager;
            _multiTenancyConfig = multiTenancyConfig;
            _logInManager = logInManager;
            _sessionAppService = sessionAppService;
            _languageManager = languageManager;
            _tenantCache = tenantCache;
            _tenantAppService = tenantAppService;
        }

        #region Login / Logout

        //public ActionResult Login(string returnUrl = "")
        //{
        //    if (TempData["Signup"] != null)
        //    {
        //        ViewBag.Signup = TempData["Signup"];
        //    }

        //    if (string.IsNullOrWhiteSpace(returnUrl))
        //    {
        //        returnUrl = Request.ApplicationPath;
        //    }

        //    if (Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Request.QueryString["storename"]))
        //    {
        //        StoremeyConsts.StoreName = Request.QueryString["storename"].ToString();
        //    }

        //    ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

        //    return View(
        //        new LoginFormViewModel
        //        {
        //            ReturnUrl = returnUrl,
        //            IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
        //            MultiTenancySide = AbpSession.MultiTenancySide
        //        });
        //}

        //public ActionResult Signup()
        //{
        //    TempData["Signup"] = true;
        //    return RedirectToAction("login");
        //}

        //[HttpPost]
        //[DisableAuditing]
        //public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        //{
        //    CheckModelState();

        //    //bool isExists = false;

        //    //isExists = await _tenantAppService.TenancyExistsAsync(loginModel.TenancyName);


        //    var loginResult = await GetLoginResultAsync(
        //        loginModel.UsernameOrEmailAddress,
        //        loginModel.Password,
        //        GetTenancyNameOrNull()
        //    );

        //    //if (!isExists)
        //    //    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, loginModel.UsernameOrEmailAddress, StoremeyConsts.StoreName);




        //    await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);


        //    if (string.IsNullOrWhiteSpace(returnUrl))
        //    {
        //        returnUrl = Request.ApplicationPath;
        //    }

        //    if (!string.IsNullOrWhiteSpace(returnUrlHash))
        //    {
        //        returnUrl = returnUrl + returnUrlHash;
        //    }

        //    if (returnUrl == "/")
        //    {
        //        string redirectURL = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + loginModel.TenancyName + "." + "www." + StoremeyConsts.DomainName + "/Home/point&LoadingTimeinsecond=2");
        //        returnUrl = StoremeyConsts.SequireServerType + loginModel.TenancyName + "." + "www." + StoremeyConsts.DomainName + "/Home/Loading?redirectURL=" + redirectURL;
        //    }



        //    return Json(new AjaxResponse { TargetUrl = returnUrl });
        //}



        //[HttpPost]
        //[AbpAllowAnonymous]
        //public async Task<JsonResult> TenancyExists(RegisterTenantViewModel RegiterModel)
        //{
        //    CheckModelState();

        //    bool isExists = false;

        //    isExists = await _tenantAppService.TenancyExistsAsync(RegiterModel.RegStoreName);

        //    if (isExists)
        //        return Json(new AjaxResponse { Success = true });
        //    else
        //        return Json(new AjaxResponse { Success = false });
        //}


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<JsonResult> Registertenant(RegisterTenantViewModel RegiterModel, string returnUrl = "", string returnUrlHash = "")
        //{
        //    CheckModelState();
        //    StoremeyConsts.tenantPassword = RegiterModel.RegPassword;
        //    string previousTenant = StoremeyConsts.tenantName;
        //    StoremeyConsts.tenantName = string.Empty;
        //    StoremeyConsts.tenantUserName = RegiterModel.RegName;

        //    StoremeyConsts.tenanEmail = RegiterModel.RegEmail;

        //    Hangfire.BackgroundJob.Enqueue(() => CreateNewRegisterAsync(RegiterModel, returnUrl, returnUrlHash, previousTenant));


        //    //if (returnUrl == "/")
        //    //{
        //    string redirectURL = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + RegiterModel.RegStoreName + "." + "www." + StoremeyConsts.DomainName + "/Account/login?storename=" + RegiterModel.RegStoreName + "&LoadingTimeinsecond=30");
        //    returnUrl = StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName + "/Home/Loading?redirectURL=" + redirectURL;
        //    //}

        //    return Json(new AjaxResponse { TargetUrl = returnUrl });
        //}


        //public async Task CreateNewRegisterAsync(RegisterTenantViewModel RegiterModel, string returnUrl = "", string returnUrlHash = "", string previousTenant = "")
        //{


        //    //var loginResult = await GetLoginResultAsync(
        //    // "admin",
        //    // "123qwe",
        //    // "Default"
        //    //);

        //    //await SignInAsync(loginResult.User, loginResult.Identity, false);


        //    CreateTenantDto CTD = new CreateTenantDto
        //    {
        //        TenancyName = RegiterModel.RegStoreName,
        //        Name = RegiterModel.RegName,
        //        AdminEmailAddress = RegiterModel.RegEmail,
        //        IsActive = true,
        //        ConnectionString = string.Empty
        //    };


        //    await _tenantAppService.CreateTeant(CTD);

        //    StoremeyConsts.tenantName = RegiterModel.RegStoreName;

        //    if (string.IsNullOrWhiteSpace(returnUrl))
        //    {
        //        returnUrl = Request.ApplicationPath;
        //    }

        //    if (!string.IsNullOrWhiteSpace(returnUrlHash))
        //    {
        //        returnUrl = returnUrl + returnUrlHash;
        //    }

        //    if (returnUrl == "/")
        //    {
        //        string redirectURL = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + RegiterModel.RegStoreName + "." + "www." + StoremeyConsts.DomainName + "/Account/login?storename=" + RegiterModel.RegStoreName + "&LoadingTimeinsecond=150;");
        //        returnUrl = StoremeyConsts.SequireServerType + RegiterModel.RegStoreName + "." + "www." + StoremeyConsts.DomainName + "/Home/Loading?redirectURL=" + redirectURL;
        //    }

        //    StoremeyConsts.tenantName = previousTenant;
        //}

        //private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        //{
        //    var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);

        //    switch (loginResult.Result)
        //    {
        //        case AbpLoginResultType.Success:
        //            return loginResult;
        //        default:
        //            throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
        //    }
        //}

        //private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        //{
        //    if (identity == null)
        //    {
        //        identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
        //    }

        //    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

        //    // Gp - fix code for NOT using session cookies
        //    // Don’t rely solely on browser behaviour for proper clean-up of session cookies during a given browsing session. 
        //    // It’s safer to use non-session cookies (IsPersistent == true) with an expiration date for having a 
        //    // consistent behaviour across all browsers and versions.
        //    // See http://blog.petersondave.com/cookies/Session-Cookies-in-Chrome-Firefox-and-Sitecore/

        //    // Gp Commented out: AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
        //    if (rememberMe)
        //    {
        //        //var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id.ToString());
        //        AuthenticationManager.SignIn(
        //            new AuthenticationProperties { IsPersistent = true },
        //            identity /*, rememberBrowserIdentity*/);
        //    }
        //    else
        //    {
        //        AuthenticationManager.SignIn(
        //            new AuthenticationProperties
        //            {
        //                IsPersistent = true,
        //                ExpiresUtc =
        //                    DateTimeOffset.UtcNow.AddMinutes(int.Parse(ConfigurationManager.AppSettings["AuthSession.ExpireTimeInMinutes.WhenNotPersistet"] ?? "30"))
        //            },
        //            identity);
        //    }
        //}

        //private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        //{
        //    switch (result)
        //    {
        //        case AbpLoginResultType.Success:
        //            return new ApplicationException("Don't call this method with a success result!");
        //        case AbpLoginResultType.InvalidUserNameOrEmailAddress:
        //        case AbpLoginResultType.InvalidPassword:
        //            return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
        //        case AbpLoginResultType.InvalidTenancyName:
        //            return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
        //        case AbpLoginResultType.TenantIsNotActive:
        //            return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
        //        case AbpLoginResultType.UserIsNotActive:
        //            return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
        //        case AbpLoginResultType.UserEmailIsNotConfirmed:
        //            return new UserFriendlyException(L("LoginFailed"), "UserEmailIsNotConfirmedAndCanNotLogin");
        //        case AbpLoginResultType.LockedOut:
        //            return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
        //        default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
        //            Logger.Warn("Unhandled login fail reason: " + result);
        //            return new UserFriendlyException(L("LoginFailed"));
        //    }
        //}

        //public ActionResult Logout()
        //{
        //    AuthenticationManager.SignOut();
        //    return RedirectToAction("Login");
        //}

        #endregion

        #region External Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(
                provider,
                Url.Action(
                    "ExternalLoginCallback",
                    "Account",
                    new
                    {
                        ReturnUrl = returnUrl
                    })
            );
        }

        //public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string tenancyName = "")
        //{
        //    var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
        //    if (loginInfo == null)
        //    {
        //        return RedirectToAction("Login");
        //    }

        //    //Try to find tenancy name
        //    if (tenancyName.IsNullOrEmpty())
        //    {
        //        var tenants = await FindPossibleTenantsOfUserAsync(loginInfo.Login);
        //        switch (tenants.Count)
        //        {
        //            case 0:
        //                return await RegisterView(loginInfo);
        //            case 1:
        //                tenancyName = tenants[0].TenancyName;
        //                break;
        //            default:
        //                return View("TenantSelection", new TenantSelectionViewModel
        //                {
        //                    Action = Url.Action("ExternalLoginCallback", "Account", new { returnUrl }),
        //                    Tenants = tenants.MapTo<List<TenantSelectionViewModel.TenantInfo>>()
        //                });
        //        }
        //    }

        //    var loginResult = await _logInManager.LoginAsync(loginInfo.Login, tenancyName);

        //    switch (loginResult.Result)
        //    {
        //        case AbpLoginResultType.Success:
        //            await SignInAsync(loginResult.User, loginResult.Identity);

        //            if (string.IsNullOrWhiteSpace(returnUrl))
        //            {
        //                returnUrl = Url.Action("Index", "Home");
        //            }

        //            return Redirect(returnUrl);
        //        case AbpLoginResultType.UnknownExternalLogin:
        //            return await RegisterView(loginInfo, tenancyName);
        //        default:
        //            throw CreateExceptionForFailedLoginAttempt(loginResult.Result, loginInfo.Email ?? loginInfo.DefaultUserName, tenancyName);
        //    }
        //}

        //private async Task<ActionResult> RegisterView(ExternalLoginInfo loginInfo, string tenancyName = null)
        //{
        //    var name = loginInfo.DefaultUserName;
        //    var surname = loginInfo.DefaultUserName;

        //    var extractedNameAndSurname = TryExtractNameAndSurnameFromClaims(loginInfo.ExternalIdentity.Claims.ToList(), ref name, ref surname);

        //    var viewModel = new RegisterViewModel
        //    {
        //        EmailAddress = loginInfo.Email,
        //        Name = name,
        //        Surname = surname,
        //        IsExternalLogin = true
        //    };

        //    if (!tenancyName.IsNullOrEmpty() && extractedNameAndSurname)
        //    {
        //        return await Register(viewModel);
        //    }

        //    return RegisterView(viewModel);
        //}

        protected virtual async Task<List<Tenant>> FindPossibleTenantsOfUserAsync(UserLoginInfo login)
        {
            List<User> allUsers;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                allUsers = await _userManager.FindAllAsync(login);
            }

            return allUsers
                .Where(u => u.TenantId != null)
                .Select(u => AsyncHelper.RunSync(() => _tenantManager.FindByIdAsync(u.TenantId.Value)))
                .ToList();
        }

        private static bool TryExtractNameAndSurnameFromClaims(List<Claim> claims, ref string name, ref string surname)
        {
            string foundName = null;
            string foundSurname = null;

            var givennameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName);
            if (givennameClaim != null && !givennameClaim.Value.IsNullOrEmpty())
            {
                foundName = givennameClaim.Value;
            }

            var surnameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname);
            if (surnameClaim != null && !surnameClaim.Value.IsNullOrEmpty())
            {
                foundSurname = surnameClaim.Value;
            }

            if (foundName == null || foundSurname == null)
            {
                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
                if (nameClaim != null)
                {
                    var nameSurName = nameClaim.Value;
                    if (!nameSurName.IsNullOrEmpty())
                    {
                        var lastSpaceIndex = nameSurName.LastIndexOf(' ');
                        if (lastSpaceIndex < 1 || lastSpaceIndex > (nameSurName.Length - 2))
                        {
                            foundName = foundSurname = nameSurName;
                        }
                        else
                        {
                            foundName = nameSurName.Substring(0, lastSpaceIndex);
                            foundSurname = nameSurName.Substring(lastSpaceIndex);
                        }
                    }
                }
            }

            if (!foundName.IsNullOrEmpty())
            {
                name = foundName;
            }

            if (!foundSurname.IsNullOrEmpty())
            {
                surname = foundSurname;
            }

            return foundName != null && foundSurname != null;
        }

        #endregion

        #region Common private methods

        private async Task<Tenant> GetActiveTenantAsync(string tenancyName)
        {
            var tenant = await _tenantManager.FindByTenancyNameAsync(tenancyName);
            if (tenant == null)
            {
                throw new UserFriendlyException(L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
            }

            if (!tenant.IsActive)
            {
                throw new UserFriendlyException(L("TenantIsNotActive", tenancyName));
            }

            return tenant;
        }

        //private string GetTenancyNameOrNull()
        //{
        //    if (!AbpSession.TenantId.HasValue)
        //    {
        //        return null;
        //    }

        //    return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        //}

        #endregion

        #region Common Partial Views


        [ChildActionOnly]
        public PartialViewResult TenantChange()
        {
            var loginInformations = AsyncHelper.RunSync(() => _sessionAppService.GetCurrentLoginInformations());

            return PartialView("_TenantChange", new TenantChangeViewModel
            {
                Tenant = loginInformations.Tenant
            });
        }

        public async Task<PartialViewResult> TenantChangeModal()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            return PartialView("_TenantChangeModal", new TenantChangeModalViewModel
            {
                TenancyName = loginInfo.Tenant?.TenancyName
            });
        }


        [ChildActionOnly]
        public PartialViewResult _AccountLanguages()
        {
            var model = new LanguageSelectionViewModel
            {
                CurrentLanguage = _languageManager.CurrentLanguage,
                Languages = _languageManager.GetLanguages().Where(l => !l.IsDisabled).ToList()
                    .Where(l => !l.IsDisabled)
                    .ToList(),
                CurrentUrl = Request.Path
            };

            return PartialView("_AccountLanguages", model);
        }

        #endregion
    }
}
