using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCashRegisterInOut.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCashRegisterInOutOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }


        public string Transaction { get; set; }
        public long? CashRegisterId { get; set; }
        public long? UserId { get; set; }
        public decimal Amount { get; set; }

        public virtual string Date { get; set; }

    }
}