using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreGeneralSettings.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreGeneralSettingsOutputDto
    {
        public virtual int TotalOutlets { get; set; }
        public virtual int TotalResiters { get; set; }
        public virtual int TotalUsers { get; set; }
        public virtual long CurrancyId { get; set; }
        public virtual bool MasterRecordEntry { get; set; }
        public virtual int MaxFileStorage { get; set; }
        public virtual string Date { get; set; }

    }
}