using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreProductSeasonLinks.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreProductSeasonLinksOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public long? ProductId { get; set; }
        public long? SeasonId { get; set; }
        public virtual string Date { get; set; }

    }
}