using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminSMTPsettings.Dto
{
    public class DeleteAdminSMTPsettingsInputDto
    {
        public virtual long Id { get; set; }

        public virtual string Host { get; set; }
        public virtual string Port { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual bool EnableSSL { get; set; }
        public virtual string TestEmail { get; set; }


        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}