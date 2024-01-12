using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTimeZones.Dto
{
    public class GetStoreTimeZonesInputDto
    {
        public virtual long Id { get; set; }
        public string name { get; set; }
        public string current_utc_offset { get; set; }
        public bool is_currently_dst { get; set; }
        public Boolean IsSelected { get; set; }

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