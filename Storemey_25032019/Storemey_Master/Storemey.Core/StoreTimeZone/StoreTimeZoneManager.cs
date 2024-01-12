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
using Storemey.StoreTimeZones;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreTimeZones
{
    public class StoreTimeZonesManager : IStoreTimeZonesManager
    {
        private readonly IRepository<StoreTimeZones, Guid> _thisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public StoreTimeZonesManager(
            IRepository<StoreTimeZones, Guid> myMasterRepository,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IEnumerable<StoreTimeZones>> ListAll()
        {
            var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false);
            return @MasterCountries;
        }


        public async Task<int?> GetRecordCount()
        {
            var @MasterCountries = _thisRpository.GetAll();

            return @MasterCountries.Count();
        }

        public async Task<IQueryable<StoreTimeZones>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {

                var @MasterCountries = _thisRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.IsDeleted == false && SqlFunctions.StringConvert((double)x.Id).Contains(SearchText)
                                             || (x.name.Contains(SearchText) || x.current_utc_offset.Contains(SearchText))
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
                            case "name":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.name);
                                break;
                            case "current_utc_offset":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.current_utc_offset);
                                break;
                            case "is_currently_dst":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.is_currently_dst);
                                break;
                            case "isSelected":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.IsSelected);
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
                            case "name":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.name);
                                break;
                            case "current_utc_offset":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.current_utc_offset);
                                break;
                            case "is_currently_dst":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.is_currently_dst);
                                break;
                            case "isSelected":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.IsSelected);
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

        public async Task Create(StoreTimeZones input)
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

        public async Task CreateOrUpdate(StoreTimeZones input)
        {
            //try
            //{

            if (_thisRpository.FirstOrDefaultAsync(x => x.name == input.name) != null)
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

        public async Task Update(StoreTimeZones input)
        {
            input.IsDeleted = false;
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreTimeZones input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreTimeZones> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreTimeZones();
            }
            return @MasterCountries;
        }

    }
}