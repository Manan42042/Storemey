using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCashRegister.Dto
{
    public class GetStoreCashRegisterInputDto
    {
        public virtual long Id { get; set; }

        public long? OutletId { get; set; }
        public long? RegisterId { get; set; }
        public long? StartBy { get; set; }
        public long? CloseBy { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public decimal CashAmount { get; set; }
        public decimal CardAmount { get; set; }
        public decimal GiftCardAmount { get; set; }
        public decimal TotalAmount { get; set; }


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