using Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly OnlineShopDbContext onlineShopDbContext;

        public ProductRepository(OnlineShopDbContext onlineShopDbContext)
        {
            this.onlineShopDbContext = onlineShopDbContext;
        }
        public async Task<Product> GetAsync(int id)
        {
            return await onlineShopDbContext.Products.FindAsync(id);
        }

        public Task<List<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertAsync(Product product)
        {
            await onlineShopDbContext.AddAsync(product);
            //await onlineShopDbContext.Products.AddAsync(product);

           //await onlineShopDbContext.SaveChangesAsync();

            return product.Id;
        }
    }
}
