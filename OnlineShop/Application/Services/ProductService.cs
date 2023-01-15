using AutoMapper;
using Infrastructure.Dto;
using Microsoft.EntityFrameworkCore;

public class ProductService : IProductService
{
    private readonly OnlineShopDbContext dbContext;
    private readonly IMapper mapper;

    //repository ??
    //unit of work ??

    public ProductService(OnlineShopDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    //DTO => Data Transfer Object
    public async Task<ProductDto> Add(ProductDto model)
    {
        //ProductDto to Product AutoMapper ??

        var product = mapper.Map<Product>(model);

        // var product = new Product{
        //     ProductName = model.ProductName,
        //     Price = model.Price,
        // };

        //dbContext.Products.Add(product);
        await dbContext.AddAsync(product);
        await dbContext.SaveChangesAsync();

        model.Id = product.Id;

        return model;
    }

    public async Task<ProductDto> Get(int id)
    {
        var product = await dbContext.Products.FindAsync(id);
        var model = mapper.Map<ProductDto>(product);

        // var model = new ProductDto
        // {
        //     Id = product.Id,
        //     Price = product.Price,
        //     ProductName = product.ProductName,
        //     PriceWithComma = product.Price.ToString("###.###"),
        // };
        return model;
    }

    public async Task<List<ProductDto>> GetAll()
    {
        //    var result = await dbContext.Products.Select(product => new ProductDto{
        //        Id = product.Id,
        //         Price = product.Price,
        //         ProductName = product.ProductName,
        //         PriceWithComma = product.Price.ToString("###.###"),
        //    }).ToListAsync();

        var products = await dbContext.Products.ToListAsync();

        var result = mapper.Map<List<ProductDto>>(products);

        return result;
    }
}