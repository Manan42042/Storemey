﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreCountryMaster.Dto
{
    public class UpdateStoreCountryMasterInputDto
    {
        public virtual long Id { get; set; }
        public virtual string CountryName { get; set; }
        public virtual string CountryCode { get; set; }
        public virtual string Note { get; set; }

        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }

    }
}