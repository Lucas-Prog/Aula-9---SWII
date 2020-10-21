using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WideWorldImporters.API.Models
{
#pragma warning disable CS1591
    public static class WideWorldImportersDbContextExtensions
    {
        public static IQueryable<StockItem> GetStockItems(this WideWorldImportersDbContext dbContext, int pageSize = 10, int pageNumber = 1, int? lastEditedBy = null, int? colorID = null, int? outerPackageID = null, int? supplierID = null, int? unitPackageID = null)
        {
            var query = dbContext.StockItems.AsQueryable();

            if (lastEditedBy.HasValue)
            { query = query.Where(item => item.LastEditedBy == lastEditedBy); }

            if (colorID.HasValue)
            { query = query.Where(item => item.ColorID == colorID); }

            if (outerPackageID.HasValue)
            { query = query.Where(item => item.OuterPackageID == outerPackageID); }

            if (supplierID.HasValue)
            { query = query.Where(item => item.SupplierID == supplierID); }

            if (unitPackageID.HasValue)
            { query = query.Where(item => item.UnitPackageID == unitPackageID); }

            return query;
        }

        public static async Task<StockItem> GetStockItemsAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
            => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemID == entity.StockItemID);

        public static async Task<StockItem> GetStockItemsByStockItemNameAsync(this WideWorldImportersDbContext dbContext, StockItem entity)
            => await dbContext.StockItems.FirstOrDefaultAsync(item => item.StockItemName == entity.StockItemName);
    }

    public static class IQueryableExtensions
    {
        public static IQueryable<TModel> Paging<TModel>(this IQueryable<TModel> query, int pageSize = 0, int pageNumber = 0) where TModel : class
            => pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
    }
#pragma warning restore CS1591
}
