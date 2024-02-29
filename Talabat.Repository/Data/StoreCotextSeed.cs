using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.core.Entities;
using Talabat.core.Entities.Orders_Aggragtion;

namespace Talabat.Repository.Data
{
    public static class StoreCotextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any()) //one element inside collection
            {
                var brandData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);

                if (brands?.Count > 0)
                {
                    foreach (var brand in brands)
                    {
                        await dbContext.ProductBrands.AddAsync(brand);

                        await dbContext.SaveChangesAsync();

                    }
                }

            }
            if (!dbContext.ProductTypes.Any()) //one element inside collection
            {
                var TypeData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);

                if (types?.Count > 0)
                {
                    foreach (var type in types)
                    {
                        await dbContext.ProductTypes.AddAsync(type);

                        await dbContext.SaveChangesAsync();

                    }
                }

            }
            if (!dbContext.Products.Any()) //one element inside collection
            {
                var productData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products?.Count > 0)
                {
                    foreach (var product in products)
                    {
                        await dbContext.Products.AddAsync(product);

                        await dbContext.SaveChangesAsync();

                    }
                }

            }

            if (!dbContext.DeliveryMethod.Any()) //one element inside collection
            {
                var MethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var DelivaryMethod = JsonSerializer.Deserialize<List<DeliveryMethod>>(MethodsData);

                if (DelivaryMethod?.Count > 0)
                {
                    foreach (var methods in DelivaryMethod)
                    {
                        await dbContext.DeliveryMethod.AddAsync(methods);

                        await dbContext.SaveChangesAsync();

                    }
                }

            }





        }
    }
}
