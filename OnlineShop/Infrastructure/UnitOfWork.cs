using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnlineShopDbContext onlineShopDbContext;

        public UnitOfWork(OnlineShopDbContext onlineShopDbContext)
        {
            this.onlineShopDbContext = onlineShopDbContext;
        }

        public void Dispose()
        {
            onlineShopDbContext.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await onlineShopDbContext.SaveChangesAsync();
        }
    }
}
