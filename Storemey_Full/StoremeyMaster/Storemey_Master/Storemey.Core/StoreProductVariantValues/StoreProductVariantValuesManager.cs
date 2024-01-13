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
using Storemey.StoreProductVariantValues;
using Storemey.Authorization.Users;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreProductVariantValues
{
    public class StoreProductVariantValuesManager : IStoreProductVariantValuesManager
    {
        private readonly IRepository<StoreProductVariantValues, Guid> _thisRpository;
        private readonly IRepository<StoreProductVariants.StoreProductVariants, Guid> _MythisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public StoreProductVariantValuesManager(
            IRepository<StoreProductVariantValues, Guid> myMasterRepository,
            IRepository<StoreProductVariants.StoreProductVariants, Guid> _myMasterRepository,
            IUnitOfWorkManager unitOfWorkManager,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
            _MythisRpository = _myMasterRepository;
        }

        public async Task<IEnumerable<StoreProductVariantValues>> ListAll()
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



        public async Task<IQueryable> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)

        //public async Task<IQueryable<StoreProductVariantValues>> ListAllQueryable(string SearchText, int CurrentPage, int MaxRecords, string SortColumn, string SortDirection)
        {
            try
            {
                int recordCount = 0;

                //var @MasterCountries = _thisRpository.GetAll();


                var @MasterCountries = _thisRpository.GetAll().Join(_MythisRpository.GetAll(),
                                      VValue => VValue.VariantId,
                                      variant => variant.Id,
                                      (VValue, variant) => new { VValue = VValue, variant = variant });


                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.VValue.IsDeleted == false && SqlFunctions.StringConvert((double)x.VValue.Id).Contains(SearchText)
                                           || (x.variant.VariantName.Contains(SearchText))
                                           || (x.VValue.VariantValue.Contains(SearchText))

                                             || (SqlFunctions.DateName("year", x.VValue.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.VValue.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.VValue.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.VValue.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.VValue.LastModificationTime).Trim()).Contains(SearchText));
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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.VValue.Id);
                                break;

                            case "variantName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.variant.VariantName);
                                break;

                            case "variantValue":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.VValue.VariantValue);
                                break;

                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.VValue.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.VValue.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.VValue.Id);
                                break;
                            case "variantName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.variant.VariantName);
                                break;

                            case "variantValue":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.VValue.VariantValue);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.VValue.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.VValue.LastModificationTime);
                                break;
                        }
                        break;
                }


                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new {
                    Id = x.VValue.Id,
                    VariantId = x.variant.Id,
                    VariantValue = x.VValue.VariantValue,
                    VariantName = x.variant.VariantName,
                    Note = x.VValue.Note,
                    IsActive = x.VValue.IsActive,
                    IsDeleted = x.VValue.IsDeleted,
                    DeleterUserId = x.VValue.DeleterUserId,
                    DeletionTime = x.VValue.DeletionTime,
                    LastModifierUserId = x.VValue.LastModifierUserId,
                    LastModificationTime = x.VValue.LastModificationTime,
                    CreatorUserId = x.VValue.CreatorUserId,
                    CreationTime = x.VValue.CreationTime,
                    recordsTotal = recordCount,
                });
                //var result = Tuple.Create(@MasterCountries.Skip(CurrentPage).Take(MaxRecords), recordCount);return result;
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task Create(StoreProductVariantValues input)
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

        public async Task Update(StoreProductVariantValues input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreProductVariantValues input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreProductVariantValues> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreProductVariantValues();
            }
            return @MasterCountries;
        }

    }
}