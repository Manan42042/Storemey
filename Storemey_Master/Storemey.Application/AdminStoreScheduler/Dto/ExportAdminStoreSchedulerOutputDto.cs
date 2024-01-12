using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminStoreScheduler.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminStoreSchedulerOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string StoreName { get; set; }
        public virtual bool SentEmail { get; set; }
        public virtual string FromEmail { get; set; }
        public virtual string ToEmail { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual string Status { get; set; }
        public virtual string Frequency { get; set; }
        public virtual string RecurWeekDays { get; set; }
        public virtual string RecurHours { get; set; }
        public virtual string Date { get; set; }

    }
}