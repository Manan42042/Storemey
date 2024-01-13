using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.MasterPlanPrices.Dto
{
    public class MasterPlanPricesAdvanceSearchInputDto
    {
        public virtual string SearchText { get; set; }
        public virtual int CurrentPage { get; set; }
        public virtual int MaxRecords { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }

        public virtual string sortColumn { get; set; }
        public virtual string sortDirection { get; set; }

    }
}