using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminSMTPsettings.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminSMTPsettingsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string Host { get; set; }
        public virtual string Port { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool EnableSSL { get; set; }
        public virtual string TestEmail { get; set; }

        public virtual string Date { get; set; }

    }
}