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
using Storemey.StoreStateMaster;
using Storemey.Authorization.Users;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreStateMaster
{
    public class StoreStateMasterManager : IStoreStateMasterManager
    {
        private readonly IRepository<StoreStateMaster, Guid> _thisRpository;
        private readonly IRepository<Storemey.StoreCountryMaster.StoreCountryMaster, Guid> _joinedRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public StoreStateMasterManager(
            IRepository<StoreStateMaster, Guid> myMasterRepository,
            IRepository<Storemey.StoreCountryMaster.StoreCountryMaster, Guid> joinedRpository,
            IUnitOfWorkManager unitOfWorkManager,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisRpository = myMasterRepository;
            _joinedRpository = joinedRpository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
        }

        public async Task<IEnumerable<StoreStateMaster>> ListAll()
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


        
        public async Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                int recordCount = 0;


                var @MasterCountries = _thisRpository.GetAll().Join(_joinedRpository.GetAll(),
                                      state => state.CountryId,
                                      country => country.Id,
                                      (state, country) => new { State = state, Country = country });

                //var @MasterCountries = _thisRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.State.IsDeleted == false && SqlFunctions.StringConvert((double)x.State.Id).Contains(SearchText)
                                             || (x.State.StateName.Contains(SearchText) || x.Country.CountryName.Contains(SearchText))
                                             || (SqlFunctions.DateName("year", x.State.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.State.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.State.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.State.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.State.LastModificationTime).Trim()).Contains(SearchText));
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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.State.Id);
                                break;
                            case "stateName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.State.StateName);
                                break;
                            case "countryName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Country.CountryName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.State.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.State.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.State.Id);
                                break;
                            case "stateName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.State.StateName);
                                break;
                            case "countryName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Country.CountryName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.State.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.State.LastModificationTime);
                                break;
                        }
                        break;
                }



                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x=> new {
                    Id = x.State.Id,
                    CountryId = x.State.CountryId,
                    StateName = x.State.StateName,
                    CountryName = x.Country.CountryName,
                    Note = x.State.Note,
                    IsActive = x.State.IsActive,
                    IsDeleted = x.State.IsDeleted,
                    DeleterUserId = x.State.DeleterUserId,
                    DeletionTime = x.State.DeletionTime,
                    LastModifierUserId = x.State.LastModifierUserId,
                    LastModificationTime = x.State.LastModificationTime,
                    CreatorUserId = x.State.CreatorUserId,
                    CreationTime = x.State.CreationTime,
                    recordsTotal = recordCount,
                });
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task<IEnumerable<StoreStateMaster>> ListAllByCountryID(long Id)
        {
            try
            {
                var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.CountryId == Id && x.IsDeleted == false);
                return @MasterCountries;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task Create(StoreStateMaster input)
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

        public async Task Update(StoreStateMaster input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreStateMaster input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreStateMaster> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreStateMaster();
            }
            return @MasterCountries;
        }

    }
}