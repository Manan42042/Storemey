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
using Storemey.StoreProducts;
using Storemey.Authorization.Users;
using System.Data.Entity.SqlServer;

namespace Storemey.StoreProducts
{
    public class StoreProductsManager : IStoreProductsManager
    {
        private readonly IRepository<StoreProducts, Guid> _thisRpository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly IRepository<StoreProductImages.StoreProductImages, Guid> _imageRpository;
        private readonly IRepository<StoreSuppliers.StoreSuppliers, Guid> _supplierRpository;
        private readonly IRepository<StoreProductVariantsLinks.StoreProductVariantsLinks, Guid> _variantsRpository;
        private readonly IRepository<StoreInventory.StoreInventory, Guid> _inventoryRpository;

        public StoreProductsManager(
            IRepository<StoreProducts, Guid> myMasterRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<StoreProductImages.StoreProductImages, Guid> imageRpository,
            IRepository<StoreSuppliers.StoreSuppliers, Guid> supplierRpository,
            IRepository<StoreProductVariantsLinks.StoreProductVariantsLinks, Guid> variantsRpository,
            IRepository<StoreInventory.StoreInventory, Guid> inventoryRpository,
            UserRegistrationManager userRegistrationManager
            )
        {
            _thisRpository = myMasterRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _userRegistrationManager = userRegistrationManager;
            _imageRpository = imageRpository;
            _supplierRpository = supplierRpository;
            _variantsRpository = variantsRpository;
            _inventoryRpository = inventoryRpository;
        }

        public async Task<IEnumerable<StoreProducts>> ListAll()
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
        {
            try
            {
                int recordCount = 0;


                //var @MasterCountries = _thisRpository.GetAll().Where(x => x.MainId == 0 || x.MainId == null);

                var @MasterCountries = _thisRpository.GetAll().Where(x => x.MainId == 0 || x.MainId == null).GroupJoin(_imageRpository.GetAll().Where(x=>x.IsDefault == true),
                 product => product.Id,
                 image => image.ProductId,
                 (product, image) => new { Product = product, Image = image }).GroupJoin(_supplierRpository.GetAll(),
                 product => product.Product.SupplierId,
                 supplier => supplier.Id,
                 (product, supplier) => new { Product = product.Product, Image = product.Image, supplier = supplier }).GroupJoin(_thisRpository.GetAll(),
                 product => product.Product.Id,
                 variant => variant.MainId,
                 (product, variant) => new { Product = product.Product, Image = product.Image, supplier = product.supplier, Variant = variant});


                if (!string.IsNullOrEmpty(SearchText))
                {
                    @MasterCountries = @MasterCountries.Where(x => x.Product.IsDeleted == false && SqlFunctions.StringConvert((double)x.Product.Id).Contains(SearchText)
                                             || x.Product.ProductName.Contains(SearchText)
                                             || (SqlFunctions.DateName("year", x.Product.LastModificationTime) + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.StringConvert((double)x.Product.LastModificationTime.Value.Month).TrimStart().Length) + SqlFunctions.StringConvert((double)x.Product.LastModificationTime.Value.Month).TrimStart() + "/" + SqlFunctions.Replicate("0", 2 - SqlFunctions.DateName("dd", x.Product.LastModificationTime).Trim().Length) + SqlFunctions.DateName("dd", x.Product.LastModificationTime).Trim()).Contains(SearchText));

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
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Product.Id);
                                break;
                            case "productName":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Product.ProductName);
                                break;
                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Product.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderByDescending(x => x.Product.LastModificationTime);
                                break;
                        }
                        break;
                    default:
                        switch (SortColumn)
                        {
                            case "id":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Product.Id);
                                break;
                            case "productName":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Product.ProductName);
                                break;

                            case "lastModificationTime":
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Product.LastModificationTime);
                                break;
                            default:
                                @MasterCountries = @MasterCountries.OrderBy(x => x.Product.LastModificationTime);
                                break;
                        }
                        break;
                }


                //var result = Tuple.Create(@MasterCountries.Skip(CurrentPage).Take(MaxRecords), recordCount);
                //return result;
                return @MasterCountries.Skip(CurrentPage).Take(MaxRecords).Select(x => new
                {
                    Id = x.Product.Id,
                    ProductName = x.Product.ProductName,
                    IsVariant = x.Product.IsVariant,
                    SKU = x.Product.SKU,
                    Barcode = x.Product.Barcode,
                    SupplierCode = x.Product.SupplierCode,
                    Description = x.Product.Description,
                    IsInventoryOn = x.Product.IsInventoryOn,
                    IsPOSProduct = x.Product.IsPOSProduct,
                    IsEcommarceProduct = x.Product.IsEcommarceProduct,
                    SupplierId = x.Product.SupplierId,
                    SupplierPrice = x.Product.SupplierPrice,
                    Supplier = x.supplier.FirstOrDefault().SupplierFullName,
                    Variants = x.Variant.Count() + " Variants",
                    Inventory = x.Product.ProductName,
                    Date = x.Product.LastModificationTime,
                    DefaultImage = x.Image.FirstOrDefault().Size1,
                    VariantId1 = x.Product.VariantId1,
                    VariantId2 = x.Product.VariantId2,
                    VariantId3 = x.Product.VariantId3,
                    VariantValueId1 = x.Product.VariantValueId1,
                    VariantValueId2 = x.Product.VariantValueId2,
                    VariantValueId3 = x.Product.VariantValueId3,
                    IsStoremeyProduct = x.Product.IsStoremeyProduct,
                    IsAllowtoSellOutofStock = x.Product.IsAllowtoSellOutofStock,
                    MainId = x.Product.MainId,
                    IsEnableSEO = x.Product.IsEnableSEO,
                    MetaTitle = x.Product.MetaTitle,
                    MetaDescription = x.Product.MetaDescription,
                    MetaKeyword = x.Product.MetaKeyword,

                    Note = x.Product.Note,
                    IsActive = x.Product.IsActive,
                    IsDeleted = x.Product.IsDeleted,
                    DeleterUserId = x.Product.DeleterUserId,
                    DeletionTime = x.Product.DeletionTime,
                    LastModifierUserId = x.Product.LastModifierUserId,
                    LastModificationTime = x.Product.LastModificationTime,
                    CreatorUserId = x.Product.CreatorUserId,
                    CreationTime = x.Product.CreationTime,
                    recordsTotal = recordCount,
                });

            }
            catch (Exception ex)
            {

            }
            return null;
        }


        public async Task<long> Create(StoreProducts input)
        {
            try
            {
                input.IsDeleted = false;
                await _thisRpository.InsertAsync(input);
                _unitOfWorkManager.Current.SaveChanges();
                return input.Id;
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
            return 0;
        }

        public async Task<long> Update(StoreProducts input)
        {
            await _thisRpository.UpdateAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
            return input.Id;
        }

        public async Task Delete(StoreProducts input)
        {
            await _thisRpository.DeleteAsync(input);
            _unitOfWorkManager.Current.SaveChanges();
        }


        public async Task DeleteByProductId(long ProductId)
        {
            await _thisRpository.DeleteAsync(x => x.MainId == ProductId);
            _unitOfWorkManager.Current.SaveChanges();
        }

        public async Task<StoreProducts> GetByID(long ID)
        {
            var @MasterCountries = await _thisRpository.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == ID);

            if (@MasterCountries == null)
            {
                return new StoreProducts();
            }
            return @MasterCountries;
        }

        public async Task<List<StoreProducts>> GetVariantsByProductId(long ID)
        {
            var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false && x.MainId == ID);

            if (@MasterCountries == null)
            {
                return new List<StoreProducts>();
            }
            return MasterCountries;
        }

        public async Task<List<StoreProducts>> GetVariantsByID(long ID)
        {
            var @MasterCountries = await _thisRpository.GetAllListAsync(x => x.IsDeleted == false && x.MainId == ID);

            if (@MasterCountries == null || @MasterCountries.Count == 0)
            {
                StoreProducts o1 = new StoreProducts();
                List<StoreProducts> lo1 = new List<StoreProducts>();
                lo1.Add(o1);
                return lo1;
            }
            return @MasterCountries;
        }

    }
}