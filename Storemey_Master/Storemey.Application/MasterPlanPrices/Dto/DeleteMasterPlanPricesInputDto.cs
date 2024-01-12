﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.MasterPlanPrices.Dto
{
    public class DeleteMasterPlanPricesInputDto
    {
        public virtual long Id { get; set; }
        public virtual int PriceID { get; set; }
        public virtual string PlanName { get; set; }
        public virtual int PlanID { get; set; }
        public virtual int CountryID { get; set; }
        public virtual int Amount { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual int DeleterUserId { get; set; }
        public virtual DateTime DeletionTime { get; set; }
        public virtual int LastModifierUserId { get; set; }
        public virtual DateTime LastModificationTime { get; set; }
        public virtual int CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }

    }
}