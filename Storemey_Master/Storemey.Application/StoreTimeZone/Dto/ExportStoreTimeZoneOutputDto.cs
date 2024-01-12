using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreTimeZones.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreTimeZonesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public string name { get; set; }
        public string current_utc_offset { get; set; }
        public bool is_currently_dst { get; set; }
        public Boolean IsSelected { get; set; }

        public virtual string Date { get; set; }

    }
}