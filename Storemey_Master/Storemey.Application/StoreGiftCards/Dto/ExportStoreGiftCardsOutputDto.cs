using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreGiftCards.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreGiftCardsOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? CustomerId { get; set; }
        public string GiftcardNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal CurrentAmount { get; set; }

        public virtual string Date { get; set; }

    }
}