using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.MasterPlans.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportMasterPlansOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual string PlanName { get; set; }

        [FieldHidden]

        public virtual bool IsActive { get; set; }

        public virtual string Date { get; set; }
    }
}