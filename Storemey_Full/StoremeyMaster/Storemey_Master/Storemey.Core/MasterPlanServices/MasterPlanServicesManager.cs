using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.Events.Bus;
using Abp.UI;
using Storemey.MasterPlanServices;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using System.Data.Entity.SqlServer;

namespace Storemey.MasterPlanServices
{
    public class MasterPlanServicesManager : IMasterPlanServicesManager
    {
        private readonly IRepository<MasterPlanServices, Guid> _thisRpository;
        private readonly IRepository<Storemey.MasterPlans.MasterPlans, Guid> _joinedRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MasterPlanServicesManager(
            IRepository<MasterPlanServices, Guid> myMasterRepository,
             IRepository<Storemey.MasterPlans.MasterPlans, Guid> joinedRpository,
                   IUnitOfWorkManager unitOfWorkManager)
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _joinedRpository = joinedRpository;
        }

        public List<MasterPlanServices> GetByPlanID(long ID)
        {
            var @MasterPlanServices = _thisRpository.GetAllList(x => x.IsDeleted == false && x.PlanID == ID);

            return @MasterPlanServices;
        }
        public async Task<IEnumerable<MasterPlanServices>> ListAll()
        {
            var @MasterPlanServices = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterPlanServices;
        }

        public async Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                int recordCount = 0;

                var @MasterCountries = _thisRpository.GetAll().Join(_joinedRpository.GetAll(),
                                     ps => ps.PlanID,
                                     p => p.Id,
                                     (p, ps) => new { p = p, ps = ps });

                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.p.IsDeleted == false && SqlFunctions.StringConvert((double)x.p.Id).Contains(SearchText)
                                             || x.p.ServiceName.Contains(SearchText)
                                             || x.ps.PlanName.Contains(SearchText)
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
                            case "serviceName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.p.ServiceName);
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
                            case "serviceName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.p.ServiceName);
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
                    PlanID = x.p.PlanID,
                    PlanName = x.ps.PlanName,
                    ServiceName = x.p.ServiceName,
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




        public async Task CreateOrUpdate(MasterPlanServices input)
        {
            if (_thisRpository.FirstOrDefaultAsync(x => x.ServiceName == input.ServiceName) != null)
            {
                await _thisRpository.UpdateAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
            else
            {
                await _thisRpository.InsertAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
        }


        public async Task Create(MasterPlanServices input)
        {
            input.IsDeleted = false;
            await _thisRpository.InsertAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Update(MasterPlanServices input)
        {
            input.IsDeleted = false;
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(MasterPlanServices input)
        {
            input.IsDeleted = true;
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<MasterPlanServices> GetByID(long ID)
        {
            var @MasterPlanServices = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterPlanServices == null)
            {
                return new MasterPlanServices();
            }
            return @MasterPlanServices;
        }

    }
}