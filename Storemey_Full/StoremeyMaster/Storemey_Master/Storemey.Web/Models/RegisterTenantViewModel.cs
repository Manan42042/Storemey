using System.ComponentModel.DataAnnotations;
using Abp.Auditing;

namespace Storemey.Web.Models.Account
{
    public class RegisterTenantViewModel
    {
        public string RegStoreName { get; set; }

        public string RegName { get; set; }

        public string RegEmail { get; set; }

        public string RegPassword { get; set; }

    }
}