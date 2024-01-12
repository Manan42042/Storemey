using System;
using Abp.Castle.Logging.Log4Net;
using Abp.Web;
using Castle.Facilities.Logging;
using Abp.WebApi.Validation;
using System.Threading;
using System.Web;
using System.Data.Entity;
using Storemey.EntityFramework;
using System.Configuration;
using Storemey.MultiTenancy;
using Microsoft.Owin.Security;
using System.Security.Principal;
using System.Web.Http;
using Abp.Timing;

namespace Storemey.Web
{

    public class MvcApplication : AbpWebApplication<StoremeyWebModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
            );

            base.Application_Start(sender, e);

            Clock.Provider = ClockProviders.Utc;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }




        //in global.asax or global.asax.cs
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            ErrorLogService.LogError(ex);
        }

        //common service to be used for logging errors
        public static class ErrorLogService
        {
            public static void LogError(Exception ex)
            {
                //Email developers, call fire department, log to database etc.
            }
        }

        protected override void Application_BeginRequest(Object source, EventArgs e)
        {
            try
            {
                if (HttpContext.Current.Request.IsSecureConnection.Equals(false) && HttpContext.Current.Request.IsLocal.Equals(false))
                {
                    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
                + HttpContext.Current.Request.RawUrl);
                }

                string MainURL = Context.Request.Url.ToString().ToLower();
                string MainURLHost = Context.Request.Url.Host.ToString().ToLower();

                if (Context.Request.Url.ToString().ToLower().Contains("https"))//!Context.Request.IsSecureConnection && !
                {
                    StoremeyConsts.SequireServerType = "https://";
                    //Response.Redirect(MainURL.ToString().Replace("http:", "https:"));
                }
                else
                {
                    StoremeyConsts.SequireServerType = "http://";
                }
                //StoremeyConsts.SequireServerType = "https://";

                //if (HttpContext.Current.Request.IsSecureConnection.Equals(false)) // && HttpContext.Current.Request.IsLocal.Equals(false)
                //{
                //    Response.Redirect("https://" + Request.ServerVariables["HTTP_HOST"]
                //+ HttpContext.Current.Request.RawUrl);
                //}
                Database.SetInitializer<StoremeyDbContext>(null);
                var app = (HttpApplication)source;
                var hostURL = MainURLHost;


                StoremeyConsts.tenantName = string.IsNullOrEmpty(hostURL.ToLower().Replace("www.","").ToLower().Replace(StoremeyConsts.DomainName, ""))
                    ? string.Empty
                    : hostURL.ToLower().Replace("www.", "").ToLower().Replace(StoremeyConsts.DomainName, "");


                if (StoremeyConsts.tenantName == "www" || StoremeyConsts.tenantName == "")
                {
                    StoremeyConsts.tenantName = string.Empty;
                }


                //if (StoremeyConsts.tenantName != "" && hostURL.Replace(StoremeyConsts.tenantName, "").ToLower().Replace("www.", "").ToLower().Replace( StoremeyConsts.DomainName, "") == "")
                //{
                //    StoremeyConsts.redirectToLogin = true;
                //}
                //else
                //{
                //    StoremeyConsts.redirectToLogin = false;
                //}
                StoremeyConsts.tenantName = StoremeyConsts.tenantName.Replace(".", "");

                if (!string.IsNullOrEmpty(StoremeyConsts.tenantName) && StoremeyConsts.tenantName == "StoremeyMaster" && !StoremeyConsts.tenantName.Contains("www"))
                {
                    StoremeyConsts.StoreName = string.Empty;
                }
                else
                {
                    if (StoremeyConsts.tenantName != string.Empty)
                    {
                        StoremeyConsts.StoreName = StoremeyConsts.tenantName;
                        //WindowsIdentity Identity = WindowsIdentity.GetCurrent();
                        if (!Storemey.EntityFramework.CommonEntityHelper.IsTenancyExists(StoremeyConsts.StoreName))
                        {
                            StoremeyConsts.tenantName = string.Empty;
                            StoremeyConsts.StoreName = string.Empty;
                            Response.Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);
                        }
                    }
                }

                StoremeyConsts.mainTenantName = string.Empty;

                if (Context.Request.Url.ToString().ToLower().Contains("admin.storemey") && StoremeyConsts.tenantName == "admin")
                {
                    StoremeyConsts.tenantName = "";
                    StoremeyConsts.StoreName = "";
                    StoremeyConsts.mainTenantName = "admin";
                }
                string resulte = Context.Request.Url.ToString().Replace(StoremeyConsts.tenantName, string.Empty).Replace(StoremeyConsts.DomainName, string.Empty).Replace("/", string.Empty).Replace(".", string.Empty);
                if (resulte == string.Empty)
                {
                            Response.Redirect(StoremeyConsts.SequireServerType + "www." + StoremeyConsts.DomainName);

                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}
