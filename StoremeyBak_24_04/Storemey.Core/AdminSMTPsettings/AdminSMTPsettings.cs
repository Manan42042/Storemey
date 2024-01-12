using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using Abp.Timing;
using Abp.UI;
using System;


namespace Storemey.AdminSMTPsettings
{
    [Table("AdminSMTPsettings")]
    public class AdminSMTPsettings : FullAuditedEntity<Guid>
    {
        public virtual long Id { get; set; }
        public virtual string Host { get; set; }
        public virtual string Port { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool EnableSSL { get; set; }
        public virtual string TestEmail { get; set; }

        public virtual bool IsActive { get; set; }
        public override bool IsDeleted { get; set; }
        public override long? DeleterUserId { get; set; }
        public override DateTime? DeletionTime { get; set; }
        public override long? LastModifierUserId { get; set; }
        public override DateTime? LastModificationTime { get; set; }
        public override long? CreatorUserId { get; set; }
        public override DateTime CreationTime { get; set; }

        public AdminSMTPsettings()
        {
            LastModificationTime = DateTime.UtcNow;
        }
    }
}
