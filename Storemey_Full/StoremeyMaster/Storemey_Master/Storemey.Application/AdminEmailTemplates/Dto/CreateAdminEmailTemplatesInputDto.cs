using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminEmailTemplates.Dto
{
    public class CreateAdminEmailTemplatesInputDto
    {

        public virtual long Id { get; set; }

        public virtual string FromEmail { get; set; }
        public virtual string ToEmail { get; set; }
        public virtual string CCEmail { get; set; }
        public virtual string BCCEmail { get; set; }
        public virtual string EmailKey { get; set; }
        public virtual string Subject { get; set; }
        public virtual string Body { get; set; }
        public virtual string AttachedFile { get; set; }

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