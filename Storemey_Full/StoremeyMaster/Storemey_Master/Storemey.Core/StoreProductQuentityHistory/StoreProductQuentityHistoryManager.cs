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
using Storemey.StoreProductQuentityHistory;
using Storemey.Authorization.Users;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreProductQuentityHistory
{
    public class StoreProductQuentityHistoryManager : IStoreProductQuentityHistoryManager
    {
        private readonly IRepository<StoreProductQuentityHistory, Guid> _thisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public StoreProductQuentityHistoryManager(
            IRepository<StoreProductQuentityHistory, Guid> myMasterRepository,
            IUnitOfWorkManager unitOfWorkManager,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
        }

        public async Task<IEnumerable<StoreProductQuentityHistory>> ListAll()
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

        public async Task<List<StoreProductQuentityHistory>> ListAllProductAndOutlet(long ProductId, long OutletId)
        {
            try
            {
                var @MasterCountries = _thisRpository.GetAllList(x => x.IsDeleted == false && x.ProductId == ProductId && x.OutletId == OutletId);
                if (@MasterCountries == null || @MasterCountries.Count == 0)
                {
                    StoreProductQuentityHistory o1 = new StoreProductQuentityHistory();
                    List<StoreProductQuentityHistory> l1 = new List<StoreProductQuentityHistory>();
                    l1.Add(o1);
                    @MasterCountries = l1;
                }
                return @MasterCountries.ToList();
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task<Tuple<IQueryable<StoreProductQuentityHistory>, int>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)

        //public async Task<IQueryable<StoreProductQuentityHistory>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                int recordCount = 0;

                var @MasterCountries = _thisRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                         
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
                          
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.LastModificationTime);
                                break;
                        }
                        break;
                }



                var result = Tuple.Create(@MasterCountries.Skip(CurrentPage).Take(MaxRecords), recordCount);return result;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task Create(StoreProductQuentityHistory input)
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

        public async Task Update(StoreProductQuentityHistory input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreProductQuentityHistory input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreProductQuentityHistory> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreProductQuentityHistory();
            }
            return @MasterCountries;
        }

    }
}