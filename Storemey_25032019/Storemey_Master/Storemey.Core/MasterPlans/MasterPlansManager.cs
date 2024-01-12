using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Storemey.MasterPlans;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Data.Entity.SqlServer;

namespace Storemey.MasterPlans
{
    public class MasterPlansManager : IMasterPlansManager
    {
        private readonly IRepository<MasterPlans, Guid> _thisManagerRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MasterPlansManager(
            IRepository<MasterPlans, Guid> myMasterRepository,
             IUnitOfWorkManager unitOfWorkManager)
        {
            _thisManagerRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IEnumerable<MasterPlans>> ListAll()
        {
            var @MasterPlans = await _thisManagerRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterPlans;
        }


        public async Task<int?> GetRecordCount()
        {
            var @MasterCountries = _thisManagerRpository.GetAll();

            return @MasterCountries.Count();
        }


        public async Task<IQueryable<MasterPlans>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {

                var @MasterCountries = _thisManagerRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                             || x.PlanName.Contains(SearchText)
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
                            case "planName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.PlanName);
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
                            case "planName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.PlanName);
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


        public async Task Create(MasterPlans input)
        {
            input.IsDeleted = false;
            await _thisManagerRpository.InsertAsync(input);
        }

        public async Task Update(MasterPlans input)
        {
            input.IsDeleted = false;
            await _thisManagerRpository.UpdateAsync(input);
        }


        public async Task CreateOrUpdate(MasterPlans input)
        {
            //var currenrObj = _thisManagerRpository.FirstOrDefaultAsync(x => x.PlanName == input.PlanName);
            //if (currenrObj != null)
            //{
            //    input.Id = currenrObj.Id;
            //    await _thisManagerRpository.UpdateAsync(input);
            //    _unitOfWorkManager.Current.SaveChanges();
            //}
            //else
            //{
                await _thisManagerRpository.InsertAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            //}
        }

        public async Task Delete(MasterPlans input)
        {
            input.IsDeleted = true;
            await _thisManagerRpository.DeleteAsync(input);
        }

        public async Task<MasterPlans> GetByID(long ID)
        {
            var @MasterPlans = await _thisManagerRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterPlans == null)
            {
                return new MasterPlans();
            }
            return @MasterPlans;
        }
        
    }
}