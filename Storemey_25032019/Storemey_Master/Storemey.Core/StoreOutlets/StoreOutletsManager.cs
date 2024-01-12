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
using Storemey.StoreOutlets;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreOutlets
{
    public class StoreOutletsManager : IStoreOutletsManager
    {
        private readonly IRepository<StoreOutlets, Guid> _thisRpository;
        private readonly IRepository<StoreWarehouses.StoreWarehouses, Guid> _StoreWarehousesRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public StoreOutletsManager(
            IRepository<StoreOutlets, Guid> myMasterRepository,
              IRepository<StoreWarehouses.StoreWarehouses, Guid> StoreWarehousesRpository,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _StoreWarehousesRpository = StoreWarehousesRpository;
        }

        public async Task<IEnumerable<StoreOutlets>> ListAll()
        {
            var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterCountries;
        }


        public async Task<int?> GetRecordCount()
        {
            var @MasterCountries = _thisRpository.GetAll();

            return @MasterCountries.Count();
        }

        public async Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                long recordCount = 0;
                //var @MasterCountries = _thisRpository.GetAll();


                var @MasterCountries = _thisRpository.GetAll().Join(_StoreWarehousesRpository.GetAll(),
                                      outlet => outlet.WarehouseId,
                                      warehouse => warehouse.Id,
                                      (outlet, warehouse) => new { Outlet = outlet, Warehouse = warehouse });


                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.Outlet.IsDeleted == false && SqlFunctions.StringConvert((double)x.Outlet.Id).Contains(SearchText)
                                             || (x.Outlet.OutletName.Contains(SearchText) || x.Outlet.OrderNumber.Contains(SearchText) || x.Warehouse.WarehouseName.Contains(SearchText) || x.Outlet.State.Contains(SearchText) || x.Outlet.City.Contains(SearchText) || x.Outlet.TimeZone.Contains(SearchText) || x.Outlet.ContactNumber.Contains(SearchText) || x.Outlet.Email.Contains(SearchText))
                                             || (SqlFunctions.DateName("year", x.Outlet.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.Outlet.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.Outlet.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.Outlet.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.Outlet.LastModificationTime).Trim()).Contains(SearchText));
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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.Id);
                                break;
                            case "outletName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.OutletName);
                                break;
                            case "warehouseName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Warehouse.WarehouseName);
                                break;

                            case "state":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.State);
                                break;

                            case "city":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.City);
                                break;
                            case "timeZone":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.TimeZone);
                                break;

                            case "contactNumber":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.ContactNumber);
                                break;

                            case "email":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.Email);
                                break;


                            case "orderNumber":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.OrderNumber);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Outlet.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.Id);
                                break;
                            case "outletName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.OutletName);
                                break;
                            case "orderNumber":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.OrderNumber);
                                break;


                            case "warehouseName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Warehouse.WarehouseName);
                                break;

                            case "state":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.State);
                                break;

                            case "city":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.City);
                                break;
                            case "timeZone":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.TimeZone);
                                break;

                            case "contactNumber":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.ContactNumber);
                                break;

                            case "email":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.Email);
                                break;



                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Outlet.LastModificationTime);
                                break;
                        }
                        break;
                }



                //return @MasterCountries.Skip(CurrentPage).Take(MaxRecords);
                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new {
                    Id = x.Outlet.Id,
                    OutletName = x.Outlet.OutletName,
                    WarehouseId = x.Warehouse.Id,
                    WarehouseName = x.Warehouse.WarehouseName,
                    OrderNumberPrefix = x.Outlet.OrderNumberPrefix,
                    OrderNumber = x.Outlet.OrderNumber,
                    IsEnableNagativeInventory = x.Outlet.IsEnableNagativeInventory,
                    SupplierReturnPrefix = x.Outlet.SupplierReturnPrefix,
                    SupplierReturnNumber = x.Outlet.SupplierReturnNumber,
                    Street = x.Outlet.Street,
                    Street1 = x.Outlet.Street1,
                    Country = x.Outlet.Country,
                    CountryId = x.Outlet.CountryId,
                    State = x.Outlet.State,
                    StateId = x.Outlet.StateId,
                    City = x.Outlet.City,
                    CityId = x.Outlet.CityId,
                    ZipCode = x.Outlet.ZipCode,
                    TimeZone = x.Outlet.TimeZone,
                    Email = x.Outlet.Email,
                    ContactNumber = x.Outlet.ContactNumber,
                    MobileNumber = x.Outlet.MobileNumber,
                    IsActive = x.Outlet.IsActive,
                    IsDeleted = x.Outlet.IsDeleted,
                    DeleterUserId = x.Outlet.DeleterUserId,
                    DeletionTime = x.Outlet.DeletionTime,
                    LastModifierUserId = x.Outlet.LastModifierUserId,
                    LastModificationTime = x.Outlet.LastModificationTime,
                    CreatorUserId = x.Outlet.CreatorUserId,
                    CreationTime = x.Outlet.CreationTime,
                    recordsTotal = recordCount,
                });
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task Create(StoreOutlets input)
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

        public async Task CreateOrUpdate(StoreOutlets input)
        {
            //try
            //{

            if (_thisRpository.FirstOrDefaultAsync(x => x.OutletName == input.OutletName) != null)
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

        public async Task Update(StoreOutlets input)
        {
            input.IsDeleted = false;
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreOutlets input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreOutlets> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreOutlets();
            }
            return @MasterCountries;
        }

    }
}