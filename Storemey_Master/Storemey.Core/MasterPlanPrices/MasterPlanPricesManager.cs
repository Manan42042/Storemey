using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Storemey.MasterPlanPrices;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Data.Entity.SqlServer;

namespace Storemey.MasterPlanPrices
{
    public class MasterPlanPricesManager : IMasterPlanPricesManager
    {
        private readonly IRepository<MasterPlanPrices, Guid> _thisManagerRpository;
        private readonly IRepository<Storemey.MasterPlans.MasterPlans, Guid> _joinedRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MasterPlanPricesManager(
            IRepository<MasterPlanPrices, Guid> myMasterRepository,
              IRepository<Storemey.MasterPlans.MasterPlans, Guid> joinedRpository,
               IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisManagerRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _joinedRpository = joinedRpository;
        }

        public async Task<IEnumerable<MasterPlanPrices>> ListAll()
        {
            var @MasterPlanPrices = await _thisManagerRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterPlanPrices;
        }



        public async Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {

                int recordCount = 0;

                var @MasterCountries = _thisManagerRpository.GetAll().Join(_joinedRpository.GetAll(),
                                     ps => ps.PlanID,
                                     p => p.Id,
                                     (p, ps) => new { p = p, ps = ps });


                //var @MasterCountries = _thisManagerRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.p.IsDeleted == false && SqlFunctions.StringConvert((double)x.p.Id).Contains(SearchText)
                                             || x.p.Amount.ToString().Contains(SearchText)
                                             || x.ps.PlanName.ToString().Contains(SearchText)
                                             || (SqlFunctions.DateName("year", x.p.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.p.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.p.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.p.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.p.LastModificationTime).Trim()).Contains(SearchText));

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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.p.Id);
                                break;
                            case "amount":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.p.Amount);
                                break;
                            case "planName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.ps.PlanName);
                                break;

                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.p.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.p.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.p.Id);
                                break;
                            case "amount":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.p.Amount);
                                break;

                            case "planName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.ps.PlanName);
                                break;

                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.p.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.p.LastModificationTime);
                                break;
                        }
                        break;
                }



                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new
                {
                    Id = x.p.Id,
                    PriceID = x.p.PriceID,
                    CountryID = x.p.CountryID,
                    PlanID = x.p.PlanID,
                    Amount = x.p.Amount,
                    PlanName = x.ps.PlanName,
                    IsActive = x.p.IsActive,
                    IsDeleted = x.p.IsDeleted,
                    DeleterUserId = x.p.DeleterUserId,
                    DeletionTime = x.p.DeletionTime,
                    LastModifierUserId = x.p.LastModifierUserId,
                    LastModificationTime = x.p.LastModificationTime,
                    CreatorUserId = x.p.CreatorUserId,
                    CreationTime = x.p.CreationTime,
                    recordsTotal = recordCount,
                });
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task Create(MasterPlanPrices input)
        {
            input.IsDeleted = false;
            await _thisManagerRpository.InsertAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Update(MasterPlanPrices input)
        {
            input.IsDeleted = false;
            await _thisManagerRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(MasterPlanPrices input)
        {
            input.IsDeleted = true;
            await _thisManagerRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task CreateOrUpdate(MasterPlanPrices input)
        {
            if (_thisManagerRpository.FirstOrDefaultAsync(x => x.PlanID == input.PlanID && x.CountryID == input.CountryID) != null)
            {
                await _thisManagerRpository.UpdateAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
            else
            {
                await _thisManagerRpository.InsertAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
        }

        public async Task<MasterPlanPrices> GetByID(long ID)
        {
            var @MasterPlanPrices = await _thisManagerRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterPlanPrices == null)
            {
                return new MasterPlanPrices();
            }
            return @MasterPlanPrices;
        }
        public MasterPlanPrices GetByPlanIDAndCountryID(long countryId, long planID)
        {
            var @MasterPlanPrices = _thisManagerRpository.FirstOrDefault(x => x.IsDeleted == false && x.CountryID == countryId && x.PlanID == planID);

            if (@MasterPlanPrices == null)
            {
                return new MasterPlanPrices();
            }
            return @MasterPlanPrices;
        }
    }
}