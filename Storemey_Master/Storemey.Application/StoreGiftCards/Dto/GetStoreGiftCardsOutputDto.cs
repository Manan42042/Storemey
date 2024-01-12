using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreGiftCards.Dto
{
    public class GetStoreGiftCardsOutputDto
    {
        public virtual long Id { get; set; }


        public long? CustomerId { get; set; }
        public string GiftcardNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CurrentAmount { get; set; }

        public int CardSold { get; set; }
        public decimal TotalAmountSum { get; set; }
        public decimal RedeemedAmountSum { get; set; }
        public decimal CurrentAmountSum { get; set; }




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