using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.MasterCountries.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportMasterCountriesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Dail_Code { get; set; }
        public virtual string Currency_Name { get; set; }
        public virtual string Curreny_Symbol { get; set; }
        public virtual string Current_Code { get; set; }

        [FieldHidden]
        public virtual string Flagimage { get; set; }
        public virtual string Date { get; set; }

    }
}