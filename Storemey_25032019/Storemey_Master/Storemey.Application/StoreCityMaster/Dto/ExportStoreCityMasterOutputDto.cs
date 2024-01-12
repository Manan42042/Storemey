using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCityMaster.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportStoreCityMasterOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string CityName { get; set; }
        public virtual string Zipcode { get; set; }

        public virtual string Date { get; set; }

    }
}