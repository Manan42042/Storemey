using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTaxGroupMapping.Dto
{
    public class GetStoreTaxGroupMappingOutputDto
    {
        public virtual long Id { get; set; }
        public virtual int TaxId { get; set; }
        public virtual int TaxGroupId { get; set; }
        public virtual string Note { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public virtual int? recordsTotal { get; set; }
    }
}