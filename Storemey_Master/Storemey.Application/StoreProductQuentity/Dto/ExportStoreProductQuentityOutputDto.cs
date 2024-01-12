using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductQuentity.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductQuentityOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? OutletId { get; set; }
        public long? Quentity { get; set; }

        public virtual string Date { get; set; }

    }
}