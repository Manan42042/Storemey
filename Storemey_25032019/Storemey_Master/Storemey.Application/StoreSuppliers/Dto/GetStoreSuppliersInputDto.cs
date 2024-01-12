﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.StoreSuppliers.Dto
{
    public class GetStoreSuppliersInputDto
    {
        public virtual long Id { get; set; }
        public virtual string SupplierFullName { get; set; }
        public virtual string Description { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Fax { get; set; }
        public virtual string Website { get; set; }
        public virtual string Address { get; set; }
        public virtual string PostCode { get; set; }
        public virtual string Country { get; set; }
        public virtual string State { get; set; }
        public virtual string City { get; set; }


        /// <summary>
        /// Default Records
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