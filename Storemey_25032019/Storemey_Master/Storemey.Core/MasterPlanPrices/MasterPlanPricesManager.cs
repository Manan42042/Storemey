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
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MasterPlanPricesManager(
            IRepository<MasterPlanPrices, Guid> myMasterRepository,
               IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisManagerRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IEnumerable<MasterPlanPrices>> ListAll()
        {
            var @MasterPlanPrices = await _thisManagerRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterPlanPrices;
        }


        public async Task<int?> GetRecordCount()
        {
            var @MasterCountries = _thisManagerRpository.GetAll();

            return @MasterCountries.Count();
        }

        public async Task<IQueryable<MasterPlanPrices>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {

                var @MasterCountries = _thisManagerRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                             || x.Amount.ToString().Contains(SearchText)
                                             || (SqlFunctions.DateName("year", x.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.LastModificationTime).Trim()).Contains(SearchText));
                }



                switch (SortDirection)
                {
                    case "desc":
                        Console.WriteLine("This is part of outer switch ");

                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Id);
                                break;
                            case "amount":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Amount);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Id);
                                break;
                            case "amount":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Amount);
                                break;

                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.LastModificationTime);
                                break;
                        }
                        break;
                }



                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords);
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
            var @MasterPlanPrices = await _thisManagerRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.CountryID == ID);

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