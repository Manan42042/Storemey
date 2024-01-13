using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductQuentityHistory.Dto
{
    public class CreateStoreProductQuentityHistoryInputDto
    {

        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? OutletId { get; set; }
        public long? OldValue { get; set; }
        public long? NewValue { get; set; }
        public long? Margin { get; set; }
        public long? FinalQuentity { get; set; }
        public string ActionDetail { get; set; }
        public long? ActionById { get; set; }
        public string ActionByName { get; set; }
        public DateTime? ActionDateTime { get; set; }



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