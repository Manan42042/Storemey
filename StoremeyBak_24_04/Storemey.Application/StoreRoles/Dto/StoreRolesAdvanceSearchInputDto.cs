using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreRoles.Dto
{
    public class StoreRolesAdvanceSearchInputDto
    {
        public virtual string SearchText { get; set; }
        public virtual int CurrentPage { get; set; }
        public virtual int MaxRecords { get; set; }
        public virtual DateTime? StartDate { get; set; }
        public virtual DateTime? EndDate { get; set; }
        public virtual string SortColumn { get; set; }
        public virtual string SortDirection { get; set; }
        public virtual int? PageNumber { get; set; }
        public virtual int? TempID { get; set; }

    }
}