using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.Net.Mail;
using Storemey.EntityFramework;

namespace Storemey.Migrations.SeedData
{
    public class DefaultSettingsCreator
    {
        private readonly StoremeyDbContext _context;

        public DefaultSettingsCreator(StoremeyDbContext context)
        {
            _context = context;
        }

        public void Create(string TenantName)
        {
            //Emailing
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, TenantName + "@storemey.com");
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, TenantName + ".storemey.com mailer");

            //Languages
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "en");
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}