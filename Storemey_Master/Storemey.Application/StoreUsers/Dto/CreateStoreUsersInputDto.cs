using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreUsers.Dto
{
    public class CreateStoreUsersInputDto
    {
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Image { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string NormalPassword { get; set; }
        public virtual string Note { get; set; }

  public virtual long? ABPUserId { get; set; }

        // Required For All Tables

        public virtual long Id { get; set; }
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