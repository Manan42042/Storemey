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
using Storemey.StoreTaxGroupLinks;
using Storemey.Authorization.Users;

namespace Storemey.StoreTaxGroupLinks
{
    public class StoreTaxGroupLinksManager : IStoreTaxGroupLinksManager
    {
        private readonly IRepository<StoreTaxGroupLinks, Guid> _thisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;

        public StoreTaxGroupLinksManager(
            IRepository<StoreTaxGroupLinks, Guid> myMasterRepository,
            IUnitOfWorkManager unitOfWorkManager,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
        }

        public async Task<IEnumerable<StoreTaxGroupLinks>> ListAll()
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
        public async Task<IEnumerable<StoreTaxGroupLinks>> GetByGroupID(int groupId)
        {
            try
            {
                var @MasterCountries = _thisRpository.GetAll().Where(x => x.IsDeleted == false && x.TaxGroupId == groupId);
                return @MasterCountries;
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public async Task Create(StoreTaxGroupLinks input)
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

        public async Task Update(StoreTaxGroupLinks input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task Delete(StoreTaxGroupLinks input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }
        public async Task DeleteByGroupID(int groupId)
        {

            var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false && x.TaxGroupId == groupId);

            foreach (var item in @MasterCountries)
            {
                await _thisRpository.DeleteAsync(item);
                _unitOfWorkManager.Current.SaveChanges();
            }
        }

        public async Task<StoreTaxGroupLinks> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreTaxGroupLinks();
            }
            return @MasterCountries;
        }

    }
}