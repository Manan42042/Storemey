using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCountryMaster.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCountryMasterOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Note { get; set; }

        public virtual string Date { get; set; }

    }
}