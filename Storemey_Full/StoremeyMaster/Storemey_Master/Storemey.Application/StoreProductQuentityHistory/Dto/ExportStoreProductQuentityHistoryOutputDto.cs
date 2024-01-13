using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductQuentityHistory.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductQuentityHistoryOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? OutletId { get; set; }
        public long? OldValue { get; set; }
        public long? NewValue { get; set; }
        public long? Margin { get; set; }
        public long? FinalQuentity { get; set; }
        public string ActionDetail { get; set; }
        public long? ActionById { get; set; }
        public string ActionByName { get; set; }
        public DateTime? ActionDateTime { get; set; }


        public virtual string Date { get; set; }

    }
}