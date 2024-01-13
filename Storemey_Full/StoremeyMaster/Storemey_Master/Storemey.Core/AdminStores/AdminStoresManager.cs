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
using System.Data.Entity.Validation;

namespace Storemey.AdminStores
{
    public class AdminStoresManager : IAdminStoresManager
    {
        private readonly IRepository<AdminStores, Guid> _thisManagerRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AdminStoresManager(
            IRepository<AdminStores, Guid> myMasterRepository,
               IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisManagerRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IEnumerable<AdminStores>> ListAll()
        {
            var @MasterPlanPrices = await _thisManagerRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterPlanPrices;
        }


        public async Task<Tuple<IQueryable<AdminStores>, int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                int recordCount = 0;

                var @MasterCountries = _thisManagerRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                             || x.StoreName.Contains(SearchText)
                                             || x.UserName.Contains(SearchText)
                                             || x.Email.Contains(SearchText)
                                             || x.PlanID.ToString().Contains(SearchText)
                                             || x.ExpirationDate.ToString().Contains(SearchText)
                                             || (SqlFunctions.DateName("year", x.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.LastModificationTime).Trim()).Contains(SearchText));
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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Id);
                                break;
                            case "storeName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.StoreName);
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
                            case "storeName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.StoreName);
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


                var result = Tuple.Create(@MasterCountries.Skip(CurrentPage).Take(MaxRecords), recordCount);
                return result;
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task Create(AdminStores input)
        {
            input.IsDeleted = false;
            await _thisManagerRpository.InsertAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Update(AdminStores input)
        {
            try
            {
                
                input.IsDeleted = false;
                await _thisManagerRpository.UpdateAsync(input);
                _unitOfWorkManager.Current.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            //catch (DbEntityValidationException e) //(Exception Ex)
            //{
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
            //            eve.Entry.Entity.GetType().Name, eve.Entry.State);
            //        foreach (var ve in eve.ValidationErrors)
            //        {
            //            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
            //                ve.PropertyName, ve.ErrorMessage);
            //        }
            //    }
            //}
        }

        public async Task Delete(AdminStores input)
        {
            input.IsDeleted = true;
            await _thisManagerRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task CreateOrUpdate(AdminStores input)
        {
            if (_thisManagerRpository.FirstOrDefaultAsync(x => x.Id == input.Id) != null)
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

        public async Task<AdminStores> GetByID(long ID)
        {
            var @MasterPlanPrices = await _thisManagerRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterPlanPrices == null)
            {
                return new AdminStores();
            }
            return @MasterPlanPrices;
        }

        public async Task<AdminStores> GetByStoreName(string StoreName)
        {
            StoremeyConsts.isMaindatabse = true;
            var @MasterPlanPrices = new AdminStores();
            try
            {
                @MasterPlanPrices = await _thisManagerRpository.FirstOrDefaultAsync(x => x.StoreName == StoreName);

                if (@MasterPlanPrices == null)
                {
                    StoremeyConsts.isMaindatabse = false;
                    return new AdminStores();
                }
                StoremeyConsts.isMaindatabse = false;
                return @MasterPlanPrices;

            }
            catch (Exception ex)
            {
                StoremeyConsts.isMaindatabse = false;
                return null;
            }
            StoremeyConsts.isMaindatabse = true;
            return null;
        }

    }
}