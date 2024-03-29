﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreStateMaster.Dto
{
    public class DeleteStoreStateMasterInputDto
    {
        public virtual long Id { get; set; }
        public virtual long CountryId { get; set; }
        public virtual string StateName { get; set; }

        /// <summary>
        /// comman fields
        /// </summary>
        public virtual string Note { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual long? DeleterUserId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
        public virtual long? LastModifierUserId { get; set; }
        public virtual DateTime? LastModificationTime { get; set; }
        public virtual long? CreatorUserId { get; set; }
        public virtual DateTime CreationTime { get; set; }


    }
}