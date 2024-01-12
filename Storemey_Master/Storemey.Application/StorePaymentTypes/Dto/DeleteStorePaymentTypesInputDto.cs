using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StorePaymentTypes.Dto
{
    public class DeleteStorePaymentTypesInputDto
    {
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual int MerchantId { get; set; }
        public virtual int TerminalId { get; set; }
        public virtual string Note { get; set; }


        public virtual bool IsPOSPaymentType { get; set; }
        public virtual bool IsEcommarcePaymentType { get; set; }

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