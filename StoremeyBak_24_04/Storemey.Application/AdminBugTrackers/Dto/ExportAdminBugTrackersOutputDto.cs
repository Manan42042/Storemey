using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminBugTrackers.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminBugTrackersOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Discription { get; set; }
        public virtual string BugURL { get; set; }
        public virtual DateTime BugDate { get; set; }
        public virtual string BugFromName { get; set; }
        public virtual string BugFromEmail { get; set; }
        public virtual string BugStoreName { get; set; }
        public virtual string AttchedFileName { get; set; }
        public virtual DateTime DeadLineDate { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime BugResolveDate { get; set; }
        public virtual string BugSolvedByName { get; set; }

        public virtual string Date { get; set; }

    }
}