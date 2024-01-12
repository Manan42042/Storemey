using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StorePaymentTypes.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStorePaymentTypesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string Name { get; set; }
        public virtual int MerchantId { get; set; }
        public virtual int TerminalId { get; set; }
        public virtual string Note { get; set; }

        public virtual string Date { get; set; }

    }
}