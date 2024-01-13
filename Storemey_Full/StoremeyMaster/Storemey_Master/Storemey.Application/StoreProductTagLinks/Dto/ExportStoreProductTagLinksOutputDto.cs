using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductTagLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductTagLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }


        public long? ProductId { get; set; }

        public long? TagId { get; set; }


        public virtual string Date { get; set; }

    }
}