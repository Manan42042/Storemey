using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.MasterPlanServices.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportMasterPlanServicesOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }

        public virtual int PlanID { get; set; }

        public virtual string ServiceName { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual string Date { get; set; }


    }
}