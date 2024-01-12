﻿using FileHelpers;
using System;
using System.ComponentModel.DataAnnotations;

namespace Storemey.AdminStores.Dto
{
    [DelimitedRecord(",")]
    [IgnoreEmptyLines()]
    [IgnoreFirst()]
    public class ExportAdminStoresOutputDto
    {
        [FieldHidden]
        public virtual long Id { get; set; }
        public string Name { get; set; }
        public string StoreName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Country { get; set; }
        public string Currancy { get; set; }

        public long? CurrancyId { get; set; }
        public long? TimeZoneId { get; set; }

        public long? CountryId { get; set; }
        public string State { get; set; }
        public long? StateId { get; set; }
        public string City { get; set; }
        public long? CityId { get; set; }
        public long? ZipCode { get; set; }
        public string TimeZone { get; set; }
        public string Language { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? PaidUser { get; set; }
        public long? PlanID { get; set; }
        public long? PlanAmount { get; set; }
        public DateTime? LastPaidDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string ConnectionString { get; set; }
        public string TotalOutlets { get; set; }
        public string TotalRegisters { get; set; }


    }
}