using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSeasons.Dto
{
    public class DeleteStoreSeasonsInputDto
    {
        public virtual long Id { get; set; }

        public string SeasonName { get; set; }

        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual string Description { get; set; }

    }
}