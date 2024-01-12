using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreRoles.Dto
{
    public class CreateStoreRolesInputDto
    {

        public virtual long Id { get; set; }

        public string Name { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }

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