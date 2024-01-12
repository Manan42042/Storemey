using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Storemey.MasterCountries;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Data;
using System.Data.Entity.Validation;
using Storemey.StoreCityMaster;
using Storemey.Authorization.Users;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreCityMaster
{
    public class StoreCityMasterManager : IStoreCityMasterManager
    {
        private readonly IRepository<StoreCityMaster, Guid> _thisRpository;
        private readonly IRepository<StoreStateMaster.StoreStateMaster, Guid> _stateRpository;
        private readonly IRepository<StoreCountryMaster.StoreCountryMaster, Guid> _countryRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public StoreCityMasterManager(
            IRepository<StoreCityMaster, Guid> myMasterRepository,
            IRepository<StoreStateMaster.StoreStateMaster, Guid> stateRpository,
            IRepository<StoreCountryMaster.StoreCountryMaster, Guid> countryRpository,
            IUnitOfWorkManager unitOfWorkManager,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
            _stateRpository = stateRpository;
            _countryRpository = countryRpository;
        }

        public async Task<IEnumerable<StoreCityMaster>> ListAll()
        {
            try
            {
                var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false);
                return @MasterCountries;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task<IEnumerable<StoreCityMaster>> ListAllByStateId(long StateId)
        {
            try
            {
                var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.StateId == StateId && x.IsDeleted == false);
                return @MasterCountries;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        

        public async Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                long recordCount = 0;
                //var @MasterCountries = _thisRpository.GetAll();

                var @MasterCountries = _thisRpository.GetAll().Join(_stateRpository.GetAll(),
                      city => city.StateId,
                      state => state.Id,
                      (city, state) => new { City = city, State = state }).Join(_countryRpository.GetAll(),
                      state => state.State.CountryId,
                      country => country.Id,
                      (state, country) => new { State = state.State, Country = country, City = state.City });

                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.City.IsDeleted == false && SqlFunctions.StringConvert((double)x.City.Id).Contains(SearchText)
                                             || (x.City.CityName.Contains(SearchText) || x.State.StateName.Contains(SearchText) || x.Country.CountryName.Contains(SearchText))
                                             || (SqlFunctions.DateName("year", x.City.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.City.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.City.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.City.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.City.LastModificationTime).Trim()).Contains(SearchText));

                    recordCount = @MasterCountries.Count();
                }
                else
                {
                    recordCount = @MasterCountries.Count();

                }



                switch (SortDirection)
                {
                    case "desc":
                        Console.WriteLine("This is part of outer switch ");

                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.City.Id);
                                break;
                            case "cityName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.City.CityName);
                                break;
                            case "stateName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.State.StateName);
                                break;
                            case "countryName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Country.CountryName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.City.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.City.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.City.Id);
                                break;
                            case "cityName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.City.CityName);
                                break;
                            case "stateName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.State.StateName);
                                break;
                            case "countryName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Country.CountryName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.City.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.City.LastModificationTime);
                                break;
                        }
                        break;
                }



                //var result = Tuple.Create(@MasterCountries.Skip(CurrentPage).Take(MaxRecords), recordCount);return result;
                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new
                {
                    Id = x.City.Id,
                    CountryId = x.Country.Id,
                    StateId = x.State.Id,
                    CountryName = x.Country.CountryName,
                    CityName = x.City.CityName,
                    StateName = x.State.StateName,
                    Zipcode = x.City.Zipcode,
                    Note = x.City.Note,
                    IsActive = x.City.IsActive,
                    IsDeleted = x.City.IsDeleted,
                    DeleterUserId = x.City.DeleterUserId,
                    DeletionTime = x.City.DeletionTime,
                    LastModifierUserId = x.City.LastModifierUserId,
                    LastModificationTime = x.City.LastModificationTime,
                    CreatorUserId = x.City.CreatorUserId,
                    CreationTime = x.City.CreationTime,
                    recordsTotal = recordCount,
                });
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task Create(StoreCityMaster input)
        {
            try
            {
                input.IsDeleted = false;
                await _thisRpository.InsertAsync(input);
                _unitOfWorkManager.Current.SaveChanges();





            }
            catch (DbEntityValidationException e) //(Exception Ex)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }
        }

        public async Task Update(StoreCityMaster input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreCityMaster input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreCityMaster> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);
            if (@MasterCountries != null && @MasterCountries.StateId > 0)
            {
                @MasterCountries.CountryId = _stateRpository.FirstOrDefault(x => x.Id == @MasterCountries.StateId).CountryId;
            }
            if (@MasterCountries == null)
            {
                return new StoreCityMaster();
            }
            return @MasterCountries;
        }

    }
}