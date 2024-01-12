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
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public MasterPlanServicesManager(
            IRepository<MasterPlanServices, Guid> myMasterRepository,
                   IUnitOfWorkManager unitOfWorkManager)
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
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

        public async Task<IQueryable<MasterPlanServices>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {

                var @MasterCountries = _thisRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                             || x.ServiceName.Contains(SearchText)
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
                            case "serviceName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.ServiceName);
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
                            case "serviceName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.ServiceName);
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