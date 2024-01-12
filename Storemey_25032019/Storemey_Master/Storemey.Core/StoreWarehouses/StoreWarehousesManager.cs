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
using Storemey.StoreWarehouses;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreWarehouses
{
    public class StoreWarehousesManager : IStoreWarehousesManager
    {
        private readonly IRepository<StoreWarehouses, Guid> _thisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public StoreWarehousesManager(
            IRepository<StoreWarehouses, Guid> myMasterRepository,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IEnumerable<StoreWarehouses>> ListAll()
        {
            var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterCountries;
        }


        public async Task<int?> GetRecordCount()
        {
            var @MasterCountries = _thisRpository.GetAll();

            return @MasterCountries.Count();
        }

        public async Task<IQueryable<StoreWarehouses>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {

                var @MasterCountries = _thisRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                             || (x.WarehouseName.Contains(SearchText) || x.State.Contains(SearchText) || x.City.Contains(SearchText) || x.ContactNumber.Contains(SearchText) || x.MobileNumber.Contains(SearchText) || x.Email.Contains(SearchText))
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
                            case "warehouseName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.WarehouseName);
                                break;
                            case "state":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.State);
                                break;
                            case "city":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.City);
                                break;
                            case "contactNumber":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.ContactNumber);
                                break;
                            case "mobileNumber":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.MobileNumber);
                                break;
                            case "email":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Email);
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
                            case "warehouseName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.WarehouseName);
                                break;

                            case "state":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.State);
                                break;
                            case "city":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.City);
                                break;
                            case "contactNumber":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.ContactNumber);
                                break;
                            case "mobileNumber":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.MobileNumber);
                                break;

                            case "email":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Email);
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

        public async Task Create(StoreWarehouses input)
        {
            try
            {

                input.IsDeleted = false;
            await _thisRpository.InsertAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
            }
            catch (Exception Ex)// (DbEntityValidationException e)
            {
                //foreach (var eve in e.EntityValidationErrors)
                //{
                //    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                //        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                //    foreach (var ve in eve.ValidationErrors)
                //    {
                //        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                //            ve.PropertyName, ve.ErrorMessage);
                //    }
                //}
            }
        }

        public async Task CreateOrUpdate(StoreWarehouses input)
        {
            //try
            //{

            if (_thisRpository.FirstOrDefaultAsync(x => x.WarehouseName == input.WarehouseName) != null)
            {
                await _thisRpository.UpdateAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
            else
            {
                await _thisRpository.InsertAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
            }
            //}
            //catch (DbEntityValidationException e)
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

        public async Task Update(StoreWarehouses input)
        {
            input.IsDeleted = false;
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreWarehouses input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreWarehouses> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreWarehouses();
            }
            return @MasterCountries;
        }

    }
}