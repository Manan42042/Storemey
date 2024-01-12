using System.Configuration;

namespace Storemey
{
    public class StoremeyConsts
    {
        public const string LocalizationSourceName = "Storemey";

        public static string tenantName = string.Empty;

        public static bool isMaindatabse = false;

        public static string mainTenantName = string.Empty;

        public static string tenantPassword = string.Empty;

        public static string tenantUserName = string.Empty;

        public static string tenanEmail = string.Empty;

        public static string StoreName = string.Empty;

        public const bool MultiTenancyEnabled = true;

        public static string DomainName = ConfigurationManager.AppSettings["DomainName"].ToString();

        public static bool redirectToLogin = false;

        public static string SequireServerType = ConfigurationManager.AppSettings["SequireServerType"].ToString();//"https://";

        public static string HostIPForIIS = ConfigurationManager.AppSettings["HostIPForIIS"].ToString();

        public static string previousStorenameCheck = string.Empty;

        public static int remainingDay = 0;
        
        public static string registerJobId = string.Empty;

        public static string defaultEntriesJobId = string.Empty;

        public static string CookieName = "";

        public static bool isFirstTimeLogin = false;


        public static string loginFirstName = "";

        public static string loginLastName = "";

        public static string loginImage = "";

        public static string loginUserName = "";

        public static string defaultImage = "";

        public static string BackendErrorString = "";

    }
}