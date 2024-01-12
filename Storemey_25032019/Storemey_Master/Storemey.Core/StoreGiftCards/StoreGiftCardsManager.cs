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
using Storemey.StoreGiftCards;
using Storemey.Authorization.Users;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreGiftCards
{
    public class StoreGiftCardsManager : IStoreGiftCardsManager
    {
        private readonly IRepository<StoreGiftCards, Guid> _thisRpository;
        private readonly IRepository<StoreCustomers.StoreCustomers, Guid> _thisStoreCustomers;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public StoreGiftCardsManager(
            IRepository<StoreGiftCards, Guid> myMasterRepository,
            IRepository<StoreCustomers.StoreCustomers, Guid> thisStoreCustomers,
            IUnitOfWorkManager unitOfWorkManager,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisStoreCustomers = thisStoreCustomers;
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
        }

        public async Task<IEnumerable<StoreGiftCards>> ListAll()
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

                var @MasterCountries = _thisRpository.GetAll().Join(_thisStoreCustomers.GetAll(),
                                      GiftCard => GiftCard.CustomerId,
                                      Customer => Customer.Id,
                                      (GiftCard, Customer) => new { GiftCard = GiftCard, Customer = Customer });
                //var @MasterCountries = _thisRpository.GetAll();
                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.GiftCard.IsDeleted == false && SqlFunctions.StringConvert((double)x.GiftCard.Id).Contains(SearchText)
                                             || x.GiftCard.GiftcardNumber.Contains(SearchText)
                                             || (SqlFunctions.DateName("year", x.GiftCard.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.GiftCard.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.GiftCard.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.GiftCard.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.GiftCard.LastModificationTime).Trim()).Contains(SearchText));
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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.GiftCard.Id);
                                break;
                            case "giftcardNumber":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.GiftCard.GiftcardNumber);
                                break;
                            case "customerName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Customer.FirstName + " " + x.Customer.LastName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.GiftCard.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.GiftCard.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.GiftCard.Id);
                                break;
                            case "giftcardNumber":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.GiftCard.GiftcardNumber);
                                break;
                            case "customerName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Customer.FirstName + " " + x.Customer.LastName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.GiftCard.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.GiftCard.LastModificationTime);
                                break;
                        }
                        break;
                }



                //public long? CustomerId { get; set; }
                //public string GiftcardNumber { get; set; }
                //public decimal TotalAmount { get; set; }
                //public decimal CurrentAmount { get; set; }

                //return @MasterCountries.Skip(CurrentPage).Take(MaxRecords);

                //return @MasterCountries.Skip(CurrentPage).Take(MaxRecords);


                //mapDataquery.ForEach(c => { c.CardSold = Convert.ToDecimal(c.recordsTotal); c.TotalAmountSum = mapDataquery.Sum(x => x.TotalAmount); c.RedeemedAmountSum = mapDataquery.Sum(x => x.RedeemedAmountSum); c.CurrentAmountSum = (mapDataquery.Sum(x => x.TotalAmount) - mapDataquery.Sum(x => x.RedeemedAmountSum)); });

                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new
                {
                    CardSold = recordCount,
                    TotalAmountSum = @MasterCountries.Sum(y => y.GiftCard.TotalAmount),
                    RedeemedAmountSum = (@MasterCountries.Sum(y => y.GiftCard.TotalAmount) - @MasterCountries.Sum(y => y.GiftCard.CurrentAmount)),
                    CurrentAmountSum = @MasterCountries.Sum(y => y.GiftCard.CurrentAmount),
                    Id = x.GiftCard.Id,
                    Note = x.GiftCard.Note,
                    CustomerId = x.Customer.Id,
                    GiftcardNumber = x.GiftCard.GiftcardNumber,
                    TotalAmount = x.GiftCard.TotalAmount,
                    CurrentAmount = x.GiftCard.CurrentAmount,
                    CustomerName = x.Customer.FirstName + " " + x.Customer.LastName,
                    IsActive = x.GiftCard.IsActive,
                    IsDeleted = x.GiftCard.IsDeleted,
                    DeleterUserId = x.GiftCard.DeleterUserId,
                    DeletionTime = x.GiftCard.DeletionTime,
                    LastModifierUserId = x.GiftCard.LastModifierUserId,
                    LastModificationTime = x.GiftCard.LastModificationTime,
                    CreatorUserId = x.GiftCard.CreatorUserId,
                    CreationTime = x.GiftCard.CreationTime,
                    recordsTotal = recordCount,
                });
            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task Create(StoreGiftCards input)
        {
            try
            {

                input.CurrentAmount = input.TotalAmount;
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

        public async Task Update(StoreGiftCards input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreGiftCards input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreGiftCards> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreGiftCards();
            }
            return @MasterCountries;
        }

    }
}