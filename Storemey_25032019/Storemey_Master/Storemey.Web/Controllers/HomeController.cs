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

using Storemey.MasterPlans;
using Abp.Web.Mvc.Authorization;
using System.IO;
using Storemey.MasterCountries;
using Microsoft.Owin.Security.Cookies;
using System.Web.Security;
using Storemey.Web.CommonHelper;
using System.Web.Hosting;
using Hangfire.Storage;
using Storemey.AdminStores;

namespace Storemey.Web.Controllers
{
    //[AbpMvcAuthorize]
    public class HomeController : StoremeyControllerBase
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
        private readonly IMasterPlansAppService _MasterPlansAppService;
        private readonly IMasterCountriesAppService _MasterCountriesAppService;
        private readonly AdminStoresManager _adminStoreManager;


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }


        public HomeController(
         TenantManager tenantManager,
         UserManager userManager,
         RoleManager roleManager,
         IUnitOfWorkManager unitOfWorkManager,
         IMultiTenancyConfig multiTenancyConfig,
         LogInManager logInManager,
         ISessionAppService sessionAppService,
         TenantAppService tenantAppService,
         ILanguageManager languageManager,
          IMasterPlansAppService MasterPlansAppService,
          IMasterCountriesAppService MasterCountriesAppService,
           AdminStoresManager adminStoreManager,
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
            _MasterPlansAppService = MasterPlansAppService;
            _MasterCountriesAppService = MasterCountriesAppService;
            _adminStoreManager = adminStoreManager;
        }


        [AbpMvcAuthorize]
        public ActionResult store()
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
            {
                if (StoremeyConsts.mainTenantName.ToLower() == "admin")
                {
                    return View("~/App/Main/Admin/adminlayout/Adminlayout.cshtml");
                }
                else
                {
                    return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
                }
            }
            return Redirect("Login");
        }


        public ActionResult automaticprocessing(string urlforwarding)
        {
            string parameterdata = PasswordHelper.Aes256CbcEncrypter.Decrypt(urlforwarding);
            parameterdata = "urlforwarding=" + parameterdata;


            string gotoURL = HttpUtility.ParseQueryString(parameterdata).Get("urlforwarding");
            string LoadingTimeinsecond = HttpUtility.ParseQueryString(parameterdata).Get("LoadingTimeinsecond");

            ViewBag.LoadingTimeinsecond = (Convert.ToInt32(LoadingTimeinsecond) * 1000);
            ViewBag.urlforwarding = gotoURL;

            return View(); //Layout of the angular application.
        }


        public ActionResult Index()
        {
            return View("~/App/Main/views/layout/layout.cshtml"); //Layout of the angular application.
        }



        /// <summary>
        ///  FrontSide Pages
        /// </summary>
        /// <returns></returns>
        /// 

        #region Front_Side_Pages

        public async Task<ActionResult> Features()
        {
            if (StoremeyConsts.tenantName != "www" && !string.IsNullOrEmpty(StoremeyConsts.tenantName))
            {
                return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
            }
            if (StoremeyConsts.redirectToLogin)
            {
                if (!AuthenticationManager.User.Identity.IsAuthenticated)
                {
                    return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
                }
                return Redirect("Logout");
            }

            return View(); //Layout of the angular application.
        }

        public async Task<ActionResult> Hardware()
        {
            if (StoremeyConsts.tenantName != "www" && !string.IsNullOrEmpty(StoremeyConsts.tenantName))
            {
                return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
            }

            if (StoremeyConsts.redirectToLogin)
            {
                if (!AuthenticationManager.User.Identity.IsAuthenticated)
                {
                    return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
                }
                return Redirect("Logout");
            }

            return View(); //Layout of the angular application.
        }

        public async Task<ActionResult> SiteMaster()
        {
            if (StoremeyConsts.tenantName != "www" && !string.IsNullOrEmpty(StoremeyConsts.tenantName))
            {
                return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
            }
            //ViewBag.PlanAndService = await _MasterPlansAppService.GetPlanAndServices();

            if (StoremeyConsts.redirectToLogin)
            {
                if (!AuthenticationManager.User.Identity.IsAuthenticated)
                {
                    return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
                }
                return Redirect("Logout");
            }

            ////Hangfire.BackgroundJob.Schedule<HomeController>(x => x.testHangfire(), Abp.Timing.Clock.Now.AddSeconds(5));

            return View(); //Layout of the angular application.
        }

        public async Task<ActionResult> Prices()
        {
            if (StoremeyConsts.tenantName != "www" && !string.IsNullOrEmpty(StoremeyConsts.tenantName))
            {
                return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
            }

            string oldvalue = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;

            ViewBag.PlanAndService = await _MasterPlansAppService.GetPlanAndServices();

            StoremeyConsts.tenantName = oldvalue;
            

            if (StoremeyConsts.redirectToLogin)
            {
                if (!AuthenticationManager.User.Identity.IsAuthenticated)
                {
                    return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
                }
                return Redirect("Logout");
            }

            return View(); //Layout of the angular application.
        }

        #endregion

        /// <summary>
        ///  OLD LOGIN DESIGN
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        /// 

        #region OLD LOGIN DESIGN

        public ActionResult OLDLogin(string returnUrl = "")
        {
            if (TempData["Signup"] != null)
            {
                ViewBag.Signup = TempData["Signup"];
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Request.QueryString["storename"]))
            {
                StoremeyConsts.StoreName = Request.QueryString["storename"].ToString();
            }

            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

            return View(
                new LoginFormViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                    MultiTenancySide = AbpSession.MultiTenancySide
                });
        }


        [HttpPost]
        [DisableAuditing]
        public async Task<JsonResult> OLDLogin(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();

            //bool isExists = false;

            //isExists = await _tenantAppService.TenancyExistsAsync(loginModel.TenancyName);

            StoremeyConsts.tenantName = loginModel.TenancyName;

            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                GetTenancyNameOrNull()
            );

            //if (!isExists)
            //    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, loginModel.UsernameOrEmailAddress, StoremeyConsts.StoreName);




            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);



            System.Web.HttpCookie MyCookie =
                   System.Web.Security.FormsAuthentication.GetAuthCookie(User.Identity.Name.ToString(),
                                                                         false);
            MyCookie.Domain = "*." + ConfigurationManager.AppSettings["DomainName"];// "storemey.local";//the second level domain name
            Response.AppendCookie(MyCookie);


            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            if (returnUrl == "/")
            {
                string urlforwarding = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + loginModel.TenancyName + "." + StoremeyConsts.DomainName + "/store&LoadingTimeinsecond=2");
                returnUrl = StoremeyConsts.SequireServerType + loginModel.TenancyName + "."  + StoremeyConsts.DomainName + "/automaticprocessing?urlforwarding=" + urlforwarding;
            }



            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }
        #endregion



        /// <summary>
        ///     WORKING LOGIN AND SIGNUP
        /// </summary>
        /// <returns></returns>
        /// 



        #region WORKING LOGIN AND SIGNUP


        public ActionResult Login(string returnUrl = "")
        {
            if (StoremeyConsts.tenantName != "www" && !string.IsNullOrEmpty(StoremeyConsts.tenantName))
            {
                return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName+"/login");
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Request.QueryString["storename"]))
            {
                StoremeyConsts.StoreName = Request.QueryString["storename"].ToString();
            }

            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

            return View(
                new LoginFormViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                    MultiTenancySide = AbpSession.MultiTenancySide
                });
        }

        [HttpPost]
        [DisableAuditing]
        public async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();

            //bool isExists = false;

            //isExists = await _tenantAppService.TenancyExistsAsync(loginModel.TenancyName);


            StoremeyConsts.tenantName = loginModel.TenancyName;

            var loginResult = await GetLoginResultAsync(
                loginModel.UsernameOrEmailAddress,
                loginModel.Password,
                GetTenancyNameOrNull()
            );

            try
            {

                string oldvalue = StoremeyConsts.tenantName;
                StoremeyConsts.tenantName = string.Empty;

                var data = await _adminStoreManager.GetByStoreName(loginModel.TenancyName);

                if (data.TimeZone == string.Empty || data.Currancy == string.Empty)
                {
                    StoremeyConsts.isFirstTimeLogin = true;
                }
                else
                {
                    StoremeyConsts.isFirstTimeLogin = false;
                }

                //Thread.Sleep(1000 * 60 * 1);
                StoremeyConsts.tenantName = oldvalue;
               
            }
            catch (Exception weeeee)
            {

            }




            var result = await _tenantAppService.IsExpired(loginModel.TenancyName);

            if (result)
            {
                throw CreateExceptionForFailedLoginAttempt(AbpLoginResultType.TenantIsNotActive, loginModel.UsernameOrEmailAddress, loginModel.TenancyName);
            }


            var tenant = await _tenantManager.FindByTenancyNameAsync(loginModel.TenancyName);

            await SignInAsync(loginResult.User, loginResult.Identity, loginModel.RememberMe);



            System.Web.HttpCookie MyCookie = System.Web.Security.FormsAuthentication.GetAuthCookie(StoremeyConsts.CookieName,
                                                                         false);
            MyCookie.Domain = "*." + ConfigurationManager.AppSettings["DomainName"];// "storemey.local";//the second level domain name
            Response.AppendCookie(MyCookie);


            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }

            if (returnUrl == "/")
            {
                string urlforwarding = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + loginModel.TenancyName + "." + StoremeyConsts.DomainName + "/store&LoadingTimeinsecond=2");
                returnUrl = StoremeyConsts.SequireServerType + loginModel.TenancyName + "." + StoremeyConsts.DomainName + "/automaticprocessing?urlforwarding=" + urlforwarding;
            }



            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }


        public ActionResult Signup(string returnUrl = "")
        {
            if (StoremeyConsts.tenantName != "www" && !string.IsNullOrEmpty(StoremeyConsts.tenantName))
            {
                return Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
            }

            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (Request.QueryString.Count > 0 && !string.IsNullOrEmpty(Request.QueryString["storename"]))
            {
                StoremeyConsts.StoreName = Request.QueryString["storename"].ToString();
            }

            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;

            return View(
                new SignupViewModel
                {
                    ReturnUrl = returnUrl,
                    IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                    MultiTenancySide = AbpSession.MultiTenancySide
                });
        }


        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> Signup(RegisterTenantViewModel RegiterModel, string returnUrl = "", string returnUrlHash = "")
        {
            CheckModelState();
            StoremeyConsts.tenantPassword = RegiterModel.RegPassword;
            string previousTenant = StoremeyConsts.tenantName;
            StoremeyConsts.tenantName = string.Empty;
            StoremeyConsts.tenantUserName = RegiterModel.RegName;

            StoremeyConsts.tenanEmail = RegiterModel.RegEmail;



           StoremeyConsts.registerJobId = Hangfire.BackgroundJob.Enqueue(() =>
            
            
            CreateNewRegisterAsync(RegiterModel, returnUrl, returnUrlHash, previousTenant)
            );


            CreateTenantDto CTD = new CreateTenantDto
            {
                TenancyName = RegiterModel.RegStoreName,
                Name = RegiterModel.RegName,
                AdminEmailAddress = RegiterModel.RegEmail,
                IsActive = true,
                ConnectionString = string.Empty
            };

            await _tenantAppService.AddTeantIntoMasterDatabase(CTD);



            CommonWebHelper.CreateDirectoriesIfMissing(RegiterModel.RegStoreName);
            //if (returnUrl == "/")
            //{
            string urlforwarding = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName + "/login?storename=" + RegiterModel.RegStoreName + "&LoadingTimeinsecond=30");
            returnUrl = StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName + "/automaticprocessing?urlforwarding=" + urlforwarding;
            //}

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return Redirect("Login");

        }

        #endregion


        /// <summary>
        ///  OTHER FUNCTION WHICH IS USED FOR LOGIN AND SIGNUP
        /// </summary>
        /// <returns></returns>


        #region OTHER FUNCTION WHICH IS USED FOR LOGIN AND SIGNUP

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        [HttpPost]
        [AbpAllowAnonymous]
        public async Task<JsonResult> TenancyExists(RegisterTenantViewModel RegiterModel)
        {
            CheckModelState();

            bool isExists = false;

            isExists = await _tenantAppService.TenancyExistsAsync(RegiterModel.RegStoreName);

            if (isExists)
                return Json(new AjaxResponse { Success = true });
            else
                return Json(new AjaxResponse { Success = false });
        }

        public async Task CreateNewRegisterAsync(RegisterTenantViewModel RegiterModel, string returnUrl = "", string returnUrlHash = "", string previousTenant = "")
        {

            BackgroundJob.Delete(StoremeyConsts.registerJobId);
            StoremeyConsts.registerJobId = string.Empty;
            //var loginResult = await GetLoginResultAsync(
            // "admin",
            // "123qwe",
            // "Default"
            //);

            //await SignInAsync(loginResult.User, loginResult.Identity, false);


            CreateTenantDto CTD = new CreateTenantDto
            {
                TenancyName = RegiterModel.RegStoreName,
                Name = RegiterModel.RegName,
                AdminEmailAddress = RegiterModel.RegEmail,
                IsActive = true,
                ConnectionString = string.Empty
            };


            await _tenantAppService.CreateTeant(CTD);



            StoremeyConsts.tenantName = RegiterModel.RegStoreName;
            Storemey.EntityFramework.CommonEntityHelper.UpdateSQL();



            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = Request.ApplicationPath;
            }

            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }




            if (returnUrl == "/")
            {
                string urlforwarding = PasswordHelper.Aes256CbcEncrypter.Encrypt(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName + "/login?storename=" + RegiterModel.RegStoreName + "&LoadingTimeinsecond=150;");
                returnUrl = StoremeyConsts.SequireServerType + RegiterModel.RegStoreName + "." + StoremeyConsts.DomainName + "/automaticprocessing?urlforwarding=" + urlforwarding;
            }

            StoremeyConsts.tenantName = previousTenant;
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);


            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    return loginResult;
                default:
                    throw CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        private async Task SignInAsync(User user, ClaimsIdentity identity = null, bool rememberMe = false)
        {
            if (identity == null)
            {
                identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            }

            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            // Gp - fix code for NOT using session cookies
            // Don’t rely solely on browser behaviour for proper clean-up of session cookies during a given browsing session. 
            // It’s safer to use non-session cookies (IsPersistent == true) with an expiration date for having a 
            // consistent behaviour across all browsers and versions.
            // See http://blog.petersondave.com/cookies/Session-Cookies-in-Chrome-Firefox-and-Sitecore/

            // Gp Commented out: AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = rememberMe }, identity);
            if (rememberMe)
            {
                //var rememberBrowserIdentity = AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(user.Id.ToString());
                AuthenticationManager.SignIn(
                    new AuthenticationProperties { IsPersistent = true },
                    identity /*, rememberBrowserIdentity*/);
            }
            else
            {
                AuthenticationManager.SignIn(
                    new AuthenticationProperties
                    {
                        AllowRefresh = false,
                        IsPersistent = true,
                        ExpiresUtc =
                            DateTimeOffset.UtcNow.AddMinutes(int.Parse(ConfigurationManager.AppSettings["AuthSession.ExpireTimeInMinutes.WhenNotPersistet"] ?? "30"))
                    },
                    identity);
            }

        }

        private Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new ApplicationException("Don't call this method with a success result!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), "UserEmailIsNotConfirmedAndCanNotLogin");
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: //Can not fall to default actually. But other result types can be added in the future and we may forget to handle it
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }


        #endregion



        public FileResult Download(string filename)
        {
            string Fullfilename = System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ExportFiles/") + filename;
            byte[] fileBytes = System.IO.File.ReadAllBytes(Fullfilename);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, filename);

        }

        public async Task<string> ImportToCSV()
        {
            var postedFile = HttpContext.Request.Files[0];
            if (postedFile != null)
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("/TransactionFile/ImportFiles/");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                postedFile.SaveAs(path + Path.GetFileName(postedFile.FileName));

                //_MasterCountriesAppService.ImportFromCSV(postedFile.FileName);

                return postedFile.FileName;
            }
            return string.Empty;
        }
    }

}