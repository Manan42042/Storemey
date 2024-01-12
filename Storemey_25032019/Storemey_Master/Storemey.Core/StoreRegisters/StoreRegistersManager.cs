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
using Storemey.StoreRegisters;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreRegisters
{
    public class StoreRegistersManager : IStoreRegistersManager
    {
        private readonly IRepository<StoreRegisters, Guid> _thisRpository;
        private readonly IRepository<StoreOutlets.StoreOutlets, Guid> _StoreOutlets;
        private readonly IRepository<StoreReceiptTemplates.StoreReceiptTemplates, Guid> _StoreReceiptTemplates;

        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public StoreRegistersManager(
            IRepository<StoreRegisters, Guid> myMasterRepository,
            IRepository<StoreOutlets.StoreOutlets, Guid> StoreOutlets,
            IRepository<StoreReceiptTemplates.StoreReceiptTemplates, Guid> StoreReceiptTemplates,
            IUnitOfWorkManager unitOfWorkManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _StoreOutlets = StoreOutlets;
            _StoreReceiptTemplates = StoreReceiptTemplates;
        }

        public async Task<IEnumerable<StoreRegisters>> ListAll()
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

                var @MasterCountries = _thisRpository.GetAll().Join(_StoreOutlets.GetAll(),
                  R => R.OutletId,
                  O => O.Id,
                  (R, O) => new { R = R, O = O }).Join(_StoreReceiptTemplates.GetAll(),
                  R => R.R.ReceiptTemplateId,
                  RT => RT.Id,
                  (R, RT) => new { R = R.R, RT = RT, O = R.O });

                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.R.IsDeleted == false && SqlFunctions.StringConvert((double)x.R.Id).Contains(SearchText)
                                             || (x.R.RegisterName.Contains(SearchText) || x.O.OutletName.Contains(SearchText) || x.RT.Name.Contains(SearchText) || x.R.ReceiptNumber.Contains(SearchText) || x.R.ReceiptPrefix.Contains(SearchText) || x.R.ReceiptSuffix.Contains(SearchText))
                                             || (SqlFunctions.DateName("year", x.R.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.R.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.R.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.R.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.R.LastModificationTime).Trim()).Contains(SearchText));
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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.Id);
                                break;
                            case "registerName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.RegisterName);
                                break;
                            case "outletName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.O.OutletName);
                                break;
                            case "receipttemplatename":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.RT.Name);
                                break;
                            case "receiptNumber":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.ReceiptNumber);
                                break;
                            case "receiptPrefix":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.ReceiptPrefix);
                                break;
                            case "receiptSuffix":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.ReceiptSuffix);
                                break;

                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.R.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.Id);
                                break;
                            case "registerName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.RegisterName);
                                break;


                            case "outletName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.O.OutletName);
                                break;
                            case "receipttemplatename":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.RT.Name);
                                break;
                            case "receiptNumber":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.ReceiptNumber);
                                break;
                            case "receiptPrefix":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.ReceiptPrefix);
                                break;
                            case "receiptSuffix":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.ReceiptSuffix);
                                break;



                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.R.LastModificationTime);
                                break;
                        }
                        break;
                }




                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new
                {
                    Id = x.R.Id,
                    OutletName = x.O.OutletName,
                    ReceiptTemplateName = x.RT.Name,
                    RegisterName = x.R.RegisterName,
                    OutletId = x.R.OutletId,
                    ReceiptTemplateId = x.R.ReceiptTemplateId,
                    ReceiptNumber = x.R.ReceiptNumber,
                    ReceiptPrefix = x.R.ReceiptPrefix,
                    ReceiptSuffix = x.R.ReceiptSuffix,
                    SelectUserForNextSale = x.R.SelectUserForNextSale,
                    EmailReceipt = x.R.EmailReceipt,
                    PrintReceipt = x.R.PrintReceipt,
                    AskForNote = x.R.AskForNote,
                    PrintNoteOnReceipt = x.R.PrintNoteOnReceipt,
                    ShowDiscountOnReceipt = x.R.ShowDiscountOnReceipt,
  
                    IsActive = x.R.IsActive,
                    IsDeleted = x.R.IsDeleted,
                    DeleterUserId = x.R.DeleterUserId,
                    DeletionTime = x.R.DeletionTime,
                    LastModifierUserId = x.R.LastModifierUserId,
                    LastModificationTime = x.R.LastModificationTime,
                    CreatorUserId = x.R.CreatorUserId,
                    CreationTime = x.R.CreationTime,
                    recordsTotal = recordCount,
                });
                //return @MasterCountries.Skip(CurrentPage).Take(MaxRecords);
            }
            catch (Exception ex)
            {

            }
            return null;
        }

        public async Task Create(StoreRegisters input)
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

        public async Task CreateOrUpdate(StoreRegisters input)
        {
            //try
            //{

            if (_thisRpository.FirstOrDefaultAsync(x => x.RegisterName == input.RegisterName) != null)
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

        public async Task Update(StoreRegisters input)
        {
            input.IsDeleted = false;
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreRegisters input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreRegisters> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreRegisters();
            }
            return @MasterCountries;
        }

    }
}