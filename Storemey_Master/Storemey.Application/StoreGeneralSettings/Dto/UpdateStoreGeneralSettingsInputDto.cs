using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreGeneralSettings.Dto
{
    public class UpdateStoreGeneralSettingsInputDto
    {

        public virtual int TotalOutlets { get; set; }
        public virtual int TotalResiters { get; set; }
        public virtual int TotalUsers { get; set; }
        public virtual long CurrancyId { get; set; }
        public virtual bool MasterRecordEntry { get; set; }
        public virtual int MaxFileStorage { get; set; }


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